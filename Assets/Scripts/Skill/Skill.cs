using EnumType;
using StructType;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    [Header("SkillMetaData")]
    public SkillMetaData metaData;

    [Space]
    [Header("Skill")]
    public string skillName;

    private SkillVector skillVec;
    public SkillOverlapData overlap;

    [Header("Effect Data")]
    [SerializeField] protected GameObject effectPrefab;
    [Space]
    [SerializeField] private Vector3[] effectRotations;
    [SerializeField] private Vector3[] effectDistances;

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

    [Tooltip("스킬 계수")] [SerializeField] protected float skillPower;
    [Tooltip("스킬 쿨타임")] public float coolTime;
    [Tooltip("스킬 지속시간")] [SerializeField] private float skillDuration;

    public YieldInstruction waitTickDelay;
    public YieldInstruction waitDelay;
    public YieldInstruction waitSkillEndDelay;

    private GameObject curEffect;
    private List<GameObject> effects;
    private ISkillOwner owner;

    private float timer;
    private int effectCount;

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
        owner = _owner;

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

    public void DamageToTargets(float _skillPower) => overlap.DealDamageToTargets(curEffect.transform, skillVec, _skillPower, owner.GetDamage());

    protected abstract bool SkillCondition();
    
    public virtual void SkillStart()
    {
        owner.isSkillActive = true;

        if(effects.Count > 0)
            effects.Clear();

        effectCount = 0;
    }
    public virtual void SkillEnd()
    {
        owner.isSkillActive = false;
        effects.Add(curEffect);
        DestroyEffect();
        curEffect = null;
    }

    public abstract IEnumerator SkillRoutine();

    protected void CreateEffect(GameObject effect)
    {
        if(curEffect != null)
            effects.Add(curEffect);

        curEffect = Manager.Resources.Instantiate(effect, owner.GetTransform().position, owner.GetTransform().rotation, true);

        skillVec.forward = curEffect.transform.forward;
        skillVec.right = curEffect.transform.right;
        skillVec.up = curEffect.transform.up;

        curEffect.transform.rotation *= Quaternion.Euler(effectRotations[effectCount]);

        Vector3 position = curEffect.transform.position + 
            (owner.GetTransform().forward * effectDistances[effectCount].z) +
            (owner.GetTransform().right   * effectDistances[effectCount].x) + 
            (owner.GetTransform().up      * effectDistances[effectCount].y);

        curEffect.transform.position = position;
        effectCount++;
    }

    IEnumerator DelayCreateEffect(GameObject effect, float delay)
    {
        yield return new WaitForSeconds(delay);

        CreateEffect(effect);
    }

    public void DestroyEffect() 
    {
        foreach (var e in effects)
        {
            Manager.Resources.Destroy(e);
        }
    }

    private void SetupSkillData(SkillData data)
    {
        skillPower = data.skillPower;
        skillName = data.skillName;
        coolTime = data.coolTime;
    }

    public void ResetCoolTime()
    {
        timer = 0;
    }
}
