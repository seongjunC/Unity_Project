using UnityEngine;

public class Player_AttackBase_State : PlayerState
{
    public Player_AttackBase_State(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        anim.SetLayerWeight(1, 0);
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetLayerWeight(1, 1);
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
        base.Update();
    }
}
