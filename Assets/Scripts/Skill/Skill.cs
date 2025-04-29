using EnumType;
using StructType;
using System.Collections;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    [SerializeField] protected GameObject effectPrefab;
    private SkillVector skillVec;
    public SkillOverlapData overlap;

    [SerializeField] private Quaternion rotation;

    [Header("Tick")]
    [SerializeField] protected bool isTick;
    [SerializeField] protected float tickDelay;

    [Header("Move")]
    [SerializeField] protected bool isMove;
    [SerializeField] protected float skillSpeed;
    [SerializeField] protected Vector3 dir;

    [Header("Skill Data")]
    [Tooltip("스킬실행 후 몇초 후 데미지 판단을 시작할지")]
    [SerializeField] private float delay;

    [Tooltip("스킬 계수")][SerializeField] private float skillPower;
    [Tooltip("스킬 쿨타임")][SerializeField] private float coolTime;
    [Tooltip("스킬 지속시간")][SerializeField] private float skillDuration;

    public YieldInstruction waitTickDelay;
    public YieldInstruction waitDelay;
    public YieldInstruction waitSkillEndDelay;

    private GameObject curEffect;
    private ISkillOnwer onwer;

    private float timer = -1;

    public void Init(ISkillOnwer _owner)
    {
        onwer = _owner;

        waitTickDelay = new WaitForSeconds(tickDelay);
        waitDelay = new WaitForSeconds(delay);
        waitSkillEndDelay = new WaitForSeconds(skillDuration);
    }

    public IEnumerator CoolTimeRoutine()
    {
        while (timer >= 0)
        {
            timer -= Time.deltaTime;

            yield return null;
        }
    }

    public bool CanUse()
    {
        if (timer <= 0.01f && SkillCondition())
        {
            timer = coolTime;
            return true;
        }
        return false;
    }

    public void DamageToTargets() => overlap.DealDamageToTargets(curEffect.transform, skillVec, skillPower, onwer.GetDamage());

    protected abstract bool SkillCondition();
    
    public virtual void StartSkill()
    {

    }
    public virtual void EndSkill()
    {
        DestroyEffect();
        curEffect = null;
    }

    public abstract IEnumerator SkillRoutine();

    protected void CreateEffect(GameObject effect)
    {
        curEffect = Manager.Resources.Instantiate(effect, onwer.GetTransform().position, onwer.GetTransform().rotation, true);

        skillVec.forward = curEffect.transform.forward;
        skillVec.right = curEffect.transform.right;
        skillVec.up = curEffect.transform.up;

        curEffect.transform.rotation *= rotation;
    }

    IEnumerator DelayCreateEffect(GameObject effect, float delay)
    {
        yield return new WaitForSeconds(delay);

        CreateEffect(effect);
    }

    public void DestroyEffect() { Manager.Resources.Destroy(curEffect); }

//#if UNITY_EDITOR

//    private BoxCollider box;
//    private CapsuleCollider capsule;
//    private SphereCollider sphere;
//    private void OnValidate()
//    {
//        if (overlap.overlapType == OverlapType.Capsule)
//        {
//            capsule.gameObject.SetActive(true);
//            box.gameObject.SetActive(false);
//            sphere.gameObject.SetActive(false);
//            capsule.center = new Vector3(0, 0.5f, 0) + overlap.distance;
//            capsule.height = overlap.height;
//            capsule.radius = overlap.radius;
//        }
//        else if (overlap.overlapType == OverlapType.Box)
//        {
//            capsule.gameObject.SetActive(false);
//            box.gameObject.SetActive(true);
//            sphere.gameObject.SetActive(false);
//            box.center = new Vector3(0, 0.5f, 0) + overlap.distance;
//            box.size = overlap.boxSize;
//        }
//        else
//        {
//            capsule.gameObject.SetActive(false);
//            box.gameObject.SetActive(false);
//            sphere.gameObject.SetActive(true);
//            sphere.center = new Vector3(0, 0.5f, 0) + overlap.distance;
//            sphere.radius = overlap.radius;
//        }
//    }
//#endif
}
