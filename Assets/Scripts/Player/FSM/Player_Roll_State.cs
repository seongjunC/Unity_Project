using EnumType;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Roll_State : Player_AttackBase_State
{
    private float invincibilityTime = 0.15f;
    private YieldInstruction rollDelay = new WaitForSeconds(.1f);
    private float timer;
    private float duration = .8f;

    public Player_Roll_State(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        timer = duration;

        player.invincibility = true;

        stateTimer = invincibilityTime;

        Vector3 inputDir = input.camDir.normalized;

        if(inputDir.sqrMagnitude < 0.01f)
            inputDir = -player.transform.forward;

        Vector3 localInput = player.transform.InverseTransformDirection(inputDir);
        stateCon.StartCoroutine(DelayRoll(inputDir));

        anim.SetFloat("RollZ", localInput.z);
        anim.SetFloat("RollX", localInput.x);
    }

    public override void Exit()
    {
        base.Exit();

        player.invincibility = false;

        rb.velocity = Vector3.zero;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Transition()
    {
        if (timer <= 0)
        {
            if (input.moveDir.sqrMagnitude > 0)
                stateMachine.ChangeState(stateCon.moveState);
            else
                stateMachine.ChangeState(stateCon.idleState);
        }
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer <= 0)
            player.invincibility = false;

        if (timer >= 0)
            timer -= Time.deltaTime;
    }

    IEnumerator DelayRoll(Vector3 inputDir)
    {
        yield return rollDelay;
        Manager.Audio.PlaySound("Roll1", SoundType.Effect, Random.Range(0.3f, 0.6f));
        rb.AddForce(inputDir * player.rollForce, ForceMode.Impulse);
    }
}
