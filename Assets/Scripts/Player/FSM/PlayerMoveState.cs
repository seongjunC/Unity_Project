using UnityEngine;


public class PlayerMoveState : PlayerState
{

    public PlayerMoveState(Player _player, StateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        player.transform.Translate(input.camDir * player.moveSpeed * Time.deltaTime, Space.World);

        if (input.camDir.sqrMagnitude > 0.2f)
        {
            input.lastMoveDir = input.camDir;
        }

        if (input.camDir.sqrMagnitude > 0)
            Rotate();
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Transition()
    {
        base.Transition();

        if (input.moveDir.sqrMagnitude < .1f)
        {
            stateMachine.ChangeState(stateCon.stopState);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            stateMachine.ChangeState(stateCon.attackState);
        }
    }

    private void Rotate()
    {
        if (input.camDir.sqrMagnitude == 0) return;
        Quaternion targetRot = Quaternion.LookRotation(input.camDir);
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRot, player.rotateSpeed * Time.deltaTime);
    }
}
