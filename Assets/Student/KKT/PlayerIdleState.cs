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
        player.anim.SetBool("Idle", true);
    }

    public override void Update()
    {
        base.Transition();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.stateCon.jumpState);
        }
        else if (player.moveDir.sqrMagnitude > 0)
        {
            stateMachine.ChangeState(player.stateCon.moveState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.anim.SetBool("Idle", false);
    }
}
