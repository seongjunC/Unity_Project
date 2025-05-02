using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private int comboCount = 1;
    public bool canNextCombo;
    private float lastAttackTime;
    private float resetTime = 3;

    public PlayerAttackState(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        SetupCombo();

        Collider[] cols = Physics.OverlapSphere(player.transform.position, 10);
        
        foreach (var c in cols)
        {
            Debug.Log(c.name);
            if (!c.CompareTag("Monster")) return;

            if (c.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                Debug.Log("1");
                damagable.TakeDamage(player.attackForce[comboCount-1]);
            }
        }

        // 공격하면 공격 히트 박스 켜기
        if (player.attackHitbox != null)
            player.attackHitbox.SetActive(true);
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        base.Exit();
        comboCount++;
        lastAttackTime = Time.time;

        // 공격 히트박스 끄기
        if (player.attackHitbox != null)
        {
            player.attackHitbox.SetActive(false);
        }
    }

    public override void Transition()
    {
        base.Transition();

        if(isFinishAnim)
            stateMachine.ChangeState(stateCon.idleState);

        if(Input.GetKeyDown(KeyCode.Mouse0) && canNextCombo)
        {
            canNextCombo = false;
            comboCount++;
            SetupCombo();
        }
    }

    private void SetupCombo()
    {
        if (comboCount > 3 || Time.time >= lastAttackTime + resetTime)
            comboCount = 1;

        anim.SetInteger("ComboCount", comboCount);
        rb.AddForce(player.transform.forward * player.attackMoveForce[comboCount - 1], ForceMode.Impulse);

        lastAttackTime = Time.time;
    }
}
