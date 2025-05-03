using UnityEngine;

public class PlayerAttackState : Player_AttackBase_State
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

        canNextCombo = false;

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

        lastAttackTime = Time.time;
    }
}
