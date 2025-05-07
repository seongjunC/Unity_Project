using UnityEngine;

public class StateMachine
{
    public PlayerState currentState;
    private PlayerState newState;

    public void InitState(PlayerState newState)
    {
        currentState = newState;
        currentState.Enter();
    }

    public void ChangeState()
    {
        if (newState == null) return;

        currentState.Exit();
        Debug.Log($"현재 : {currentState.animBoolName}, 전이 : {newState.animBoolName}");
        currentState = newState;
        currentState.Enter();
    }
    public void ChangeState(PlayerState _newState)
    {
        if (_newState == null) return;

        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }

    public void SetupState(PlayerState _newState)
    {
        newState = _newState;
    }
}
