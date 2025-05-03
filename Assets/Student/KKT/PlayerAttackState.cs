using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public int comboCount = 1;
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
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        base.Exit();
        comboCount++;
        lastAttackTime = Time.time;
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
