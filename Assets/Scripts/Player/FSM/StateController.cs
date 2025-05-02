using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class StateController : MonoBehaviour 
{
    private Player player;
    public StateMachine stateMachine {  get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerStopState stopState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    //public PlayerDieState dieState { get; private set; }

    private void Awake()
    {
        player = GetComponent<Player>();

        stateMachine = new StateMachine();
                
        moveState = new PlayerMoveState(player, stateMachine, "Move");
        jumpState = new PlayerJumpState(player, stateMachine, "Jump");
        idleState = new PlayerIdleState(player, stateMachine, "Idle");
        attackState = new PlayerAttackState(player, stateMachine, "Attack");
        stopState = new PlayerStopState(player, stateMachine, "Stop");
        // dieState = new PlayerDieState(player, stateMachine, "Die");
        // 그래서 객체마다 stateMachine을 각자 가지고 있어야함
    }

    private void Start()
    {
        stateMachine.InitState(idleState); // 처음엔 상태 초기화
    }

    private void Update()
    {
        stateMachine.UpdateStateMachine(); // state자체에는 Update를 호출할 수 없어서 Controller에서 실행
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.FixedUpdate();
    }

    public void AnimFinishEvent() => stateMachine.currentState.AnimFinishEvent();
    public void CanNextComboEvent() => attackState.canNextCombo = true;   
}
