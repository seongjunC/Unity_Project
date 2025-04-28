using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// »ý¼ºµÈ state
public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

    public override void Transition()
    {
        base.Transition();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(new PlayerJumpState(player, stateMachine, "Jump"));
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (x == 0 && z == 0)
        {
            stateMachine.ChangeState(new PlayerIdleState(player, stateMachine, "Idle"));
        }
    }

    public override void Update()
    {
        base.Update();
        Move();
        Rotate();
    }

    private void Move()
    {
        float input = Input.GetAxis("Vertical");
        player.transform.Translate(Vector3.forward * player.moveSpeed * input * Time.deltaTime);
    }

    private void Rotate()
    {
        float input = Input.GetAxis("Horizontal");
        player.transform.Rotate(Vector3.up, player.rotateSpeed * input * Time.deltaTime);
    }
}
