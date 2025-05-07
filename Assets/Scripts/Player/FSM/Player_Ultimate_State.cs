using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ultimate_State : Player_AttackBase_State
{
    public Player_Ultimate_State(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
    }

    public override void Transition()
    {
        base.Transition();
    }

    public override void Update()
    {
        if (!player.isSkillActive)
            stateMachine.SetupState(stateCon.idleState);
    }
}
