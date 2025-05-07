using UnityEngine;

public class PlayerState
{
    protected Player player;              
    protected StateMachine stateMachine;  
    protected Animator anim => player.anim;
    protected Rigidbody rb => player.rigid;
    protected StateController stateCon => player.stateCon;
    protected PlayerInput input => player.input;

    public string animBoolName;
    protected float stateTimer;
    protected bool isFinishAnim;

    public PlayerState(Player _player, StateMachine _stateMachine, string _animBoolName)
    {
        player = _player;
        stateMachine = _stateMachine;
        animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        stateMachine.SetupState(null);
        isFinishAnim = false;
        player.anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            stateMachine.SetupState(stateCon.rollState);
    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);   
    }

    public virtual void Transition()
    {
        stateMachine.ChangeState();
    }

    public void AnimFinishEvent()
    {
        isFinishAnim = true;
    }
}
