using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum StateEnum { Idle, Walk, Attack, Hide }

public class Enemy : Actor, IUser, ITarget
{
    public FSM fsm;
    public State startState;

    [SerializeField] private float raycastLength;
    private Vector3[] directions;
    private Vector3 forward;
    private RaycastHit[] hit;
    private NavMeshAgent navMeshAgent;

    //This is from the ITarget interface
    Transform ITarget.player => Player.Instance.transform;
    
    //This is from the IState interface
    NavMeshAgent IUser.navMeshAgent => navMeshAgent;
    
    float IUser.rayCastLength => raycastLength;
    Vector3 IUser.forward => forward;
    Vector3[] IUser.directions => directions;
    RaycastHit[] IUser.hit => hit;

    Bomb IUser.bomb => bomb;

    private void Awake()
    {
        directions = new Vector3[] { Vector3.left, Vector3.right, Vector3.up, Vector3.back };
        forward = Vector3.forward;
        hit = new RaycastHit[directions.Length];
        Debug.Log(directions);
        navMeshAgent = GetComponent<NavMeshAgent>();

        fsm = new FSM(this, this, StateEnum.Idle, new IdleState(StateEnum.Idle), new WalkState(StateEnum.Walk), 
                    new AttackState(StateEnum.Attack), new HideState(StateEnum.Hide));
    }

    private void Update()
    {
        fsm.OnUpdate();
    }

    public override void Die()
    {
        base.Die();
        Debug.Log("Enemy " + gameID + " has died");
        ActorManager.Instance.RemoveFromList(this);
        Destroy(this.gameObject);
    }
}
