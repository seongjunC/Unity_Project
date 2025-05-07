using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (input.moveDir.sqrMagnitude > 0)
        {
            stateMachine.SetupState(stateCon.moveState);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            stateMachine.SetupState(stateCon.attackState);
        }
    }
    
    public override void Exit()
    {
        base.Exit();
    }

    public override void Transition()
    {
        base.Transition();   
    }
}
