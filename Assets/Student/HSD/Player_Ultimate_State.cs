using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ultimate_State : PlayerState
{
    public Player_Ultimate_State(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.cutSceneCon.PlayUltimateCutScnen();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Transition()
    {
        base.Transition();

        if (!player.isSkillActive)
            stateMachine.ChangeState(stateCon.idleState);
    }

    public override void Update()
    {
        base.Update();
    }
}
