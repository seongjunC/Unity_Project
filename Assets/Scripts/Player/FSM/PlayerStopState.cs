using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStopState : PlayerState
{
    public PlayerStopState(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.rigid.velocity = input.lastMoveDir * (player.moveSpeed / 2);
    }

    public override void Transition()
    {
        base.Transition();

        if (isFinishAnim)
            stateMachine.ChangeState(stateCon.idleState);

        if (input.camDir.sqrMagnitude > 0)
            stateMachine.ChangeState(stateCon.moveState);
    }

    public override void Update()
    {
        base.Update();    
    }
}
