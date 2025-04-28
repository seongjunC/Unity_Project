using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// state들이 상속받을 클래스
public class PlayerState
{
    protected Player player;              // owner에 대한 정보를 가지고 있음
    protected StateMachine stateMachine;    // 상태 전이를 위해 stateMachine을 가지고 있음
    private string animBoolName;

    public PlayerState(Player _player, StateMachine _stateMachine, string _animBoolName)
    {
        player = _player;
        stateMachine = _stateMachine;
        animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        // 상태로 들어왔을 때 실행되는 함수
    }

    public virtual void Update()
    {
        // 상태가 실행되는 동안 실행되는 함수
    }

    public virtual void Exit()
    {
        // 상태에서 나갈때 실행되는 함수
    }

    public virtual void Transition()
    {
        // 전이 될 조건과 전이 될 상태를 지정하는 함수

        //ex if(플레이어가 땅에서 스페이스바를 누르면)
        //      stateMacine.ChangeState(점프 상태);
    }
}
