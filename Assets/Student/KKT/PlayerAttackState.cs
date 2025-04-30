using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private float attackDuration = 0.5f; // 공격 지속시간
    private float attackTimer;

    public PlayerAttackState(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        attackTimer = attackDuration;

        // 공격하면 공격 히트 박스 켜기
        if (player.attackHitbox != null)
            player.attackHitbox.SetActive(true);

    }

    public override void Update()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
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

    public override void Exit()
    {
        base.Exit();

        // 공격 히트박스 끄기
        if (player.attackHitbox != null)
        {
            player.attackHitbox.SetActive(false);
        }
    }
}
