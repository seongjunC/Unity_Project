using UnityEngine;

// 생성된 state
public class PlayerMoveState : PlayerState
{
    Vector3 currentVelocity;
    Vector3 targetVelocity;
    float acceleration = 10;

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

        player.transform.Translate(player.camDir * player.moveSpeed * Time.deltaTime, Space.World);

        if (player.camDir.sqrMagnitude > 0.2f)
        {
            player.lastMoveDir = player.camDir;
        }

        if (player.camDir.sqrMagnitude > 0)
            Rotate();
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Transition()
    {
        base.Transition();

        // ex) 스페이스바를 누르면 상태 전이
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(stateCon.jumpState);
        }
        else if (player.moveDir.sqrMagnitude < .1f)
        {
            stateMachine.ChangeState(stateCon.stopState);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            stateMachine.ChangeState(stateCon.attackState);
        }
    }

    //public override void FixedUpdate()
    //{
    //    base.FixedUpdate();

    //   Vector3 targetPosition = rb.position + player.camDir * player.moveSpeed * Time.fixedDeltaTime;
    //   rb.MovePosition(targetPosition);
    //}

    private void Rotate()
    {
        if (player.camDir.sqrMagnitude == 0) return;
        Quaternion targetRot = Quaternion.LookRotation(player.camDir);
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRot, player.rotateSpeed * Time.deltaTime);
    }
}
