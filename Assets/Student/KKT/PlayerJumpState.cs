using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private Rigidbody rigid;
    private bool isGrounded = true;

    public PlayerJumpState(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        rigid = player.rigid;
    }

    public override void Enter()
    {
        base.Enter();
        Jump();
    }

    private void Jump()
    {
        rigid.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Transition()
    {
        base.Transition();

        if (player.IsGrounded())
        {
            stateMachine.ChangeState(new PlayerMoveState(player, stateMachine, "Move"));
        }
    }
}
