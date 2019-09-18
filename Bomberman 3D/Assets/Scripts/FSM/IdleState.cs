using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(StateEnum id)
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
        fsm.SwitchState(StateEnum.Walk);
    }
}
