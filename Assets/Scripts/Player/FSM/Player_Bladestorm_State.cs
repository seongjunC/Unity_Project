
 using UnityEngine;

public class Player_Bladestorm_State : Player_AttackBase_State
{
    public Player_Bladestorm_State(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        {
            if (input.moveDir.sqrMagnitude > 0)
                stateMachine.SetupState(stateCon.moveState);
            else
                stateMachine.SetupState(stateCon.idleState);
        }
    }
}
