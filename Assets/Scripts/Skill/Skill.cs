using EnumType;
using StructType;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public Sprite icon;
    public string skillName;
    private SkillVector skillVec;
    public SkillOverlapData overlap;

    [Header("Effect Data")]
    [SerializeField] protected GameObject effectPrefab;
    [Space]
    [SerializeField] private Vector3 effectRotation;
    [SerializeField] private Vector3 effectDistance;

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

    [Tooltip("스킬 계수")] [SerializeField] private float skillPower;
    [Tooltip("스킬 쿨타임")] public float coolTime;
    [Tooltip("스킬 지속시간")] [SerializeField] private float skillDuration;

    public YieldInstruction waitTickDelay;
    public YieldInstruction waitDelay;
    public YieldInstruction waitSkillEndDelay;

    private GameObject curEffect;
    private ISkillOwner onwer;

    private float timer;

    public float CoolTimeRatio
    {
        get
        {
            if (coolTime <= 0f) return 0f;
            return Mathf.Clamp01(timer / coolTime);
        }
    }

    public void Init(ISkillOwner _owner)
    {
        onwer = _owner;

        waitTickDelay = new WaitForSeconds(tickDelay);
        waitDelay = new WaitForSeconds(delay);
        waitSkillEndDelay = new WaitForSeconds(skillDuration);

        SetupSkillData(Manager.Data.skillData.GetSkillData(skillName));
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

        curEffect.transform.rotation *= Quaternion.Euler(effectRotation);

        Vector3 position = curEffect.transform.position + (curEffect.transform.forward * effectDistance.z) 
            + (curEffect.transform.right * effectDistance.x) + (curEffect.transform.up * effectDistance.y);

        curEffect.transform.position = position;
    }

    IEnumerator DelayCreateEffect(GameObject effect, float delay)
    {
        yield return new WaitForSeconds(delay);

        CreateEffect(effect);
    }

    public void DestroyEffect() { Manager.Resources.Destroy(curEffect); }

    private void SetupSkillData(SkillData data)
    {
        skillPower = data.skillPower;
        skillName = data.skillName;
        coolTime = data.coolTime;
    }
}
