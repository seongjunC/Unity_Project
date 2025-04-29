using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 생성된 state
public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        player.anim.SetBool("Move", true);
    }

    public override void Update()
    {
        base.Update();
        Move();
    }

    public override void Exit()
    {
        base.Exit();

        player.anim.SetBool("Move", false);
    }
    public override void Transition()
    {
        base.Transition();

        // ex) 스페이스바를 누르면 상태 전이
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.stateCon.jumpState);
        }
        else if (player.moveDir.sqrMagnitude == 0)
        {
            stateMachine.ChangeState(player.stateCon.idleState);
        }
    }

    // 움직임
    private void Move()
    {
        player.transform.Translate(player.moveDir*player.moveSpeed*Time.deltaTime);
    }
}
