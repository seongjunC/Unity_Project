using EnumType;
using System.Collections;
using UnityEngine;

public class Player_Roll_State : Player_AttackBase_State
{
    private float invincibilityTime = .25f;
    private YieldInstruction rollDelay = new WaitForSeconds(.1f);
    private float timer;
    private float duration = .8f;

    public Player_Roll_State(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log($"Roll {anim.GetBool("Roll")}");
        timer = duration;

        player.statusCon.invincibility = true;

        stateTimer = invincibilityTime;

        Vector3 inputDir = input.camDir.normalized;

        if(inputDir.sqrMagnitude < 0.01f)
            inputDir = -player.transform.forward;

        Vector3 localInput = player.transform.InverseTransformDirection(inputDir);

        anim.SetFloat("RollZ", localInput.z);
        anim.SetFloat("RollX", localInput.x);

        stateCon.StartCoroutine(DelayRoll(inputDir));
    }

    public override void Exit()
    {
        base.Exit();

        player.statusCon.invincibility = false;

        rb.velocity = Vector3.zero;
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
        if (stateTimer <= 0)
            player.statusCon.invincibility = false;

        if (timer >= 0)
            timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (input.moveDir.sqrMagnitude > 0)
                stateMachine.SetupState(stateCon.moveState);
            else
                stateMachine.SetupState(stateCon.idleState);
        }
    }

    IEnumerator DelayRoll(Vector3 inputDir)
    {
        yield return rollDelay;

        Manager.Audio.PlaySound("Roll1", SoundType.Effect, Random.Range(0.3f, 0.6f));

        float rollDuration = 0.6f;
        float elapsed = 0f;

        Vector3 movePerSecond = inputDir.normalized * player.rollForce;

        while (elapsed < rollDuration)
        {
            float delta = Time.deltaTime;
            player.transform.Translate(movePerSecond * delta, Space.World);
            elapsed += delta;
            yield return null;
        }
    }
}
