using UnityEngine;

public class SkeletonStateController : MonoBehaviour
{
    #region States
    public SkeletonStateMachine sm {  get; private set; }
    public Skeleton_Idle idle { get; private set; }
    public Skeleton_Chase chase { get; private set; }
    public Skeleton_Attack attack { get; private set; }

    // Skill
    public Skeleton_SwordSkill skill1 { get; private set; }
    public Skeleton_RoarSkill skill2 { get; private set; }
    #endregion

    public SkeletonBoss skeleton;

    private void Awake()
    {
        skeleton ??= GetComponent<SkeletonBoss>();

        sm = new SkeletonStateMachine();

        idle    = new Skeleton_Idle(skeleton, sm, "Idle");
        chase   = new Skeleton_Chase(skeleton, sm, "Chase");
        attack  = new Skeleton_Attack(skeleton, sm, "Attack");
        skill1  = new Skeleton_SwordSkill(skeleton, sm, "Skill1");
        skill2 = new Skeleton_RoarSkill(skeleton, sm, "Skill2");
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        sm.InitState(idle);
    }

    private void Update()
    {
        sm.UpdateStateMachine();
    }

    private void AnimFinishTrigger() => sm.currentState.AnimFinishEvent();
    private void MoveEvent(float force) => skeleton.rigid.AddForce(skeleton.transform.forward * force, ForceMode.Impulse);
}
