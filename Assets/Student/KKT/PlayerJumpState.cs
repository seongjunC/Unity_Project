using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.anim.SetBool("Jump", true);
        Jump();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Transition()
    {
        base.Transition();

        if (player.IsGrounded())
        {
            player.anim.SetBool("Jump", false);

            if (player.moveDir.sqrMagnitude > 0)
            {
                stateMachine.ChangeState(player.stateCon.moveState);
            }
            else
            {
                stateMachine.ChangeState(player.stateCon.idleState);
            }
        }
    }
    private void Jump()
    {
        player.rigid.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }
}
