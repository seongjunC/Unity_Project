using System.Collections;
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

    protected float stateTimer;

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
        if (stateTimer >= 0)
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

        if (target != null && Vector3.Distance(target.transform.position, monster.transform.position) >= 2f)
            sm.ChangeState(stateCon.chase);

        stateCon.chase.RandomAttack();
    }

    public override void Update()
    {
        base.Update();
    }
}

public class Skeleton_Chase : SkeletonState
{
    private Vector3 dirToTarget;
    private float attackDelay = 5;
    public float attackTimer;
    public Skeleton_Chase(SkeletonBoss _monster, SkeletonStateMachine _stateMachine, string _animBoolName) : base(_monster, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        attackTimer = attackDelay;

        monster.stateCon.StartCoroutine(AttackDelayRoutine());
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Transition()
    {
        base.Transition();

        if (target == null || Vector3.Distance(target.transform.position, monster.transform.position) <= 2f)
            sm.ChangeState(stateCon.idle);

        RandomAttack();
    }

    public void RandomAttack()
    {
        if (attackTimer <= 0)
        {
            if (Vector3.Distance(target.transform.position, monster.transform.position) <= monster.attackDistance &&
            Vector3.Dot(monster.transform.forward, dirToTarget) > Mathf.Cos((monster.fov / 2f) * Mathf.Deg2Rad))
            {
                int index = Random.Range(1, 4);

                switch (index)
                {
                    case 1:
                        sm.ChangeState(stateCon.attack);
                        break;
                    case 2:
                        sm.ChangeState(stateCon.skill1);
                        break;
                    case 3:
                        sm.ChangeState(stateCon.skill2);
                        break;
                }

                attackTimer = attackDelay;
            }
        }
    }

    public override void Update()
    {
        base.Update();

        Chase();
    }

    private void Chase()
    {
        if (target != null)
        {
            dirToTarget = (target.transform.position - monster.transform.position).normalized;
            dirToTarget.y = 0;

            monster.transform.rotation = Quaternion.Slerp
                (monster.transform.rotation, Quaternion.LookRotation(dirToTarget), monster.rotationSpeed * Time.deltaTime);

            monster.transform.Translate(dirToTarget * monster.statusCon.status.speed * Time.deltaTime, Space.World);
        }
    }

    IEnumerator AttackDelayRoutine()
    {
        while (attackTimer >= 0)
        {
            Debug.Log(attackTimer);
            attackTimer -= Time.deltaTime;
            yield return null;
        }
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

        if (isFinishAnim)
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

    public override void Enter()
    {
        base.Enter();
        monster.skillCon.UseSkills(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Transition()
    {
        base.Transition();

        if (isFinishAnim)
            sm.ChangeState(stateCon.chase);
    }

    public override void Update()
    {
        base.Update();
    }
}
public class Skeleton_RoarSkill : SkeletonState
{
    public Skeleton_RoarSkill(SkeletonBoss _monster, SkeletonStateMachine _stateMachine, string _animBoolName) : base(_monster, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        monster.skillCon.UseSkills(1);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Transition()
    {
        base.Transition();

        if (isFinishAnim)
            sm.ChangeState(stateCon.chase);
    }

    public override void Update()
    {
        base.Update();
    }
}

public class Skeleton_Die : SkeletonState
{
    public Skeleton_Die(SkeletonBoss _monster, SkeletonStateMachine _stateMachine, string _animBoolName) : base(_monster, _stateMachine, _animBoolName)
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
    }

    public override void Update()
    {
        base.Update();
    }
}

public class Skeleton_Hit : SkeletonState
{
    public Skeleton_Hit(SkeletonBoss _monster, SkeletonStateMachine _stateMachine, string _animBoolName) : base(_monster, _stateMachine, _animBoolName)
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

        if (isFinishAnim)
            sm.ChangeState(stateCon.idle);
    }

    public override void Update()
    {
        base.Update();
    }
}
