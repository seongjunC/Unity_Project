using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 상태의 전이, 생성을 관리하는 클래스
public class StateMachine
{
    public PlayerState currentState;

    /// <summary>
    /// 상태 초기화 함수
    /// </summary>
    /// <param name="newState"></param>
    public void InitState(PlayerState newState)
    {
        currentState = newState;
        currentState.Enter();
    }

    /// <summary>
    /// 상태 변경 함수
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(PlayerState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    /// <summary>
    /// 프레임 마다 업데이트 될 함수인 state의 Update와 Transition을 호출시키는 함수
    /// </summary>
    public void UpdateStateMachine()
    {
        currentState.Update();
        currentState.Transition();
    }
}
