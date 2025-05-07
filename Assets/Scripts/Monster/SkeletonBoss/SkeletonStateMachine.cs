using UnityEngine;

public class SkeletonStateMachine
{
    public SkeletonState currentState;

    public void InitState(SkeletonState newState)
    {
        currentState = newState;
        currentState.Enter();
    }

    public void ChangeState(SkeletonState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void UpdateStateMachine()
    {
        currentState.Update();
        currentState.Transition();
    }
}
