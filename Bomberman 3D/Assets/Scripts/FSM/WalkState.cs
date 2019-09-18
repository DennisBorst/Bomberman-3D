using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : State
{
    private Vector3 destinationPoint;
    private bool needDestination = true;
    private int _xPos;
    private int _zPos;
    private int distance;
    private int distanceToPlayer;

    public WalkState(StateEnum id)
    {
        this.id = id;
    }

    public override void OnEnter(IUser _iUser, ITarget _iTarget)
    {
        base.OnEnter(_iUser, _iTarget); 
    }

    public override void OnExit()
    {
        Debug.Log("Exit Time");
    }

    public override void OnUpdate()
    {
        //Debug.Log("Walking");
        distance = Convert.ToInt32((Vector3.Distance(_iUser.navMeshAgent.destination, destinationPoint)));
        distanceToPlayer = Convert.ToInt32((Vector3.Distance(_iUser.navMeshAgent.destination, _iTarget.player.transform.position)));
        //Debug.Log("distance = " + distance);
        //Debug.Log("distance to player = " + distanceToPlayer);
        //Debug.DrawRay(_iUser.transform.position, _iUser.transform.forward, Color.red);
        
        if(distanceToPlayer <= 10)
        {
            _iUser.navMeshAgent.destination = _iTarget.player.transform.position;
        }
        else
        {
            _iUser.navMeshAgent.destination = destinationPoint;
        }

        if (_iUser.navMeshAgent.destination == destinationPoint || distance <= 3)
        {
            needDestination = true;
        }

        if (needDestination)
        {
            SetDestination();
            needDestination = false;
        }

        if (Physics.Raycast(_iUser.transform.position, _iUser.transform.forward, out _iUser.hit[3], _iUser.rayCastLength))
        {
            if (_iUser.hit[3].collider.gameObject.tag == "Player")
            {
                Debug.DrawRay(_iUser.transform.position, _iUser.transform.forward, Color.green);
                fsm.SwitchState(StateEnum.Attack);
            }
            else if (_iUser.hit[3].collider.gameObject.tag == "Wall")
            {
                Debug.DrawRay(_iUser.transform.position, _iUser.transform.forward, Color.green);
                fsm.SwitchState(StateEnum.Attack);
            }
        }
    }

    private void SetDestination()
    {
        System.Random random = new System.Random();
        _xPos = random.Next(-13, 13);
        _zPos = random.Next(-13, 13);
        destinationPoint = new Vector3(_xPos, 0f, _zPos);
        //Debug.Log(destinationPoint);
    }
}
