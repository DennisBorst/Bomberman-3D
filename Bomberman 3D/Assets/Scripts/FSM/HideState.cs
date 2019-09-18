using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideState : State
{
    private float maxHideTime = 5f;
    private float hideTimer;

    public HideState(StateEnum id)
    {
        this.id = id;
    }

    public override void OnEnter(IUser _iUser, ITarget _iTarget)
    {
        base.OnEnter(_iUser, _iTarget);
        hideTimer = maxHideTime;
    }

    public override void OnExit()
    {
        Debug.Log("Exit Time");
    }

    public override void OnUpdate()
    {
        Debug.Log("Hiding");
        hideTimer -= Time.deltaTime;
        _iUser.transform.Translate(Vector3.back * Time.deltaTime * 5f);
        Debug.DrawRay(_iUser.transform.position, _iUser.directions[2], Color.white);
        if (hideTimer <= 0)
        {
            Debug.DrawRay(_iUser.transform.position, _iUser.directions[3], Color.yellow);
            fsm.SwitchState(StateEnum.Walk);
        }
    }
}
