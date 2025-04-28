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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Transition()
    {
        base.Transition();

        // ex) 스페이스바를 누르면 상태 전이
    }

    public override void Update()
    {
        base.Update();

        // 움직임
    }
}
