using UnityEngine;

public class SkeletonState
{
    protected SkeletonBoss monster;
    protected SkeletonStateMachine sm;
    protected SkeletonStateController stateCon => monster.stateCon;
    protected Animator anim => monster.anim;
    protected Rigidbody rb => monster.rigid;
    protected GameObject target => monster.target;

    private string animBoolName;

    protected bool isFinishAnim;

    private float stateTimer;

    public SkeletonState(SkeletonBoss _monster, SkeletonStateMachine _stateMachine, string _animBoolName)
    {
        monster = _monster;
        sm = _stateMachine;
        animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        isFinishAnim = false;

        anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        anim.SetBool(animBoolName, false);
    }

    public virtual void Update()
    {
        if(stateTimer >= 0)
            stateTimer -= Time.deltaTime;
    }

    public virtual void Transition()
    {

    }

    public void AnimFinishEvent() => isFinishAnim = true;
}

public class Skeleton_Idle : SkeletonState
{
    public Skeleton_Idle(SkeletonBoss _monster, SkeletonStateMachine _stateMachine, string _animBoolName) : base(_monster, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Transition()
    {
        base.Transition();

        if (target != null)
            sm.ChangeState(stateCon.chase);
    }

    public override void Update()
    {
        base.Update();
    }
}

public class Skeleton_Chase : SkeletonState
{
    private Vector3 dirToTarget;

    public Skeleton_Chase(SkeletonBoss _monster, SkeletonStateMachine _stateMachine, string _animBoolName) : base(_monster, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Transition()
    {
        base.Transition();

        if(target == null)
            sm.ChangeState(stateCon.idle);

        if (Vector3.Distance(target.transform.position, monster.transform.position) <= 6 &&
            Vector3.Dot(monster.transform.forward, dirToTarget) > Mathf.Cos((monster.fov / 2f) * Mathf.Deg2Rad))
        {
            sm.ChangeState(stateCon.attack);
        }
    }

    public override void Update()
    {
        base.Update();

        Chase();
    }

    private void Chase()
    {
        if(target != null)
        {
            dirToTarget = (target.transform.position - monster.transform.position).normalized;
            dirToTarget.y = 0;

            monster.transform.rotation = Quaternion.Slerp
                (monster.transform.rotation, Quaternion.LookRotation(dirToTarget), monster.rotationSpeed * Time.deltaTime);

            monster.transform.Translate(dirToTarget * monster.statusCon.status.speed * Time.deltaTime, Space.World);
        }
    }

    private void LookAtTarget(Vector3 dir)
    {
        monster.transform.rotation = Quaternion.LookRotation(dir);
    }
}

public class Skeleton_Attack : SkeletonState
{
    public Skeleton_Attack(SkeletonBoss _monster, SkeletonStateMachine _stateMachine, string _animBoolName) : base(_monster, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Transition()
    {
        base.Transition();

        if(isFinishAnim)
            sm.ChangeState(stateCon.chase);
    }

    public override void Update()
    {
        base.Update();
    }
}

public class Skeleton_SwordSkill : SkeletonState
{
    public Skeleton_SwordSkill(SkeletonBoss _monster, SkeletonStateMachine _stateMachine, string _animBoolName) : base(_monster, _stateMachine, _animBoolName)
    {
    }
}