using EnumType;
using StructType;
using System.Collections;
using System.Collections.Generic;
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
    public GameObject effectPrefab;
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
    [Tooltip("스킬실행 후 몇초 후 이펙트가 사라질지")]
    [SerializeField] private float delay;

    [Tooltip("스킬 계수")] [SerializeField] protected float skillPower;
    [Tooltip("스킬 쿨타임")] public float coolTime;
    [Tooltip("스킬 지속시간")] [SerializeField] private float skillDuration;

    public YieldInstruction waitTickDelay;
    public YieldInstruction waitDelay;
    public YieldInstruction waitSkillEndDelay;

    protected GameObject curEffect;
    private List<GameObject> effects = new List<GameObject>();
    private List<SkillVector> skillVectors = new List<SkillVector>();
    private ISkillOwner owner;

    private float timer;
    private int effectCount;
    private bool isSetup = false;

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

        waitTickDelay ??= new WaitForSeconds(tickDelay);
        waitDelay ??= new WaitForSeconds(delay);
        waitSkillEndDelay ??= new WaitForSeconds(skillDuration);

        if(!isSetup)
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

    public void DamageToTargets() => overlap.DealDamageToTargets(curEffect.transform, skillVec, skillPower, owner.GetDamage());

    public void DamageToTargets(int count) => overlap.DealDamageToTargets(effects[count].transform, skillVectors[count], skillPower, owner.GetDamage());

    public void DamageToTargets(float _skillPower) => overlap.DealDamageToTargets(curEffect.transform, skillVec, _skillPower, owner.GetDamage());

    public void DamageToTargets(float _skillPower, Vector3 overlapDistance) => overlap.DealDamageToTargets(curEffect.transform, overlapDistance, skillVec, _skillPower, owner.GetDamage());

    protected abstract bool SkillCondition();
    
    public virtual void SkillStart()
    {
        owner.isSkillActive = true;

        if(effects.Count > 0)
            effects.Clear();

        if(skillVectors.Count > 0)
            skillVectors.Clear();

        effectCount = 0;
    }
    public IEnumerator SkillEnd()
    {
        owner.isSkillActive = false;
        effects.Add(curEffect);  
        curEffect = null;

        yield return waitDelay;

        DestroyEffect();
    }

    public abstract IEnumerator SkillRoutine();

    public void CreateEffect(GameObject effect)
    {
        curEffect = Manager.Resources.Instantiate(effect, owner.GetTransform().position, owner.GetTransform().rotation, true);

        effects.Add(curEffect);

        skillVec.forward = curEffect.transform.forward;
        skillVec.right = curEffect.transform.right;
        skillVec.up = curEffect.transform.up;

        skillVectors.Add(skillVec);

        curEffect.transform.rotation *= Quaternion.Euler(effectRotations[effectCount]);

        Vector3 position = OwnerPos(effectDistances[effectCount]);

        curEffect.transform.position = position;
        effectCount++;
    }

    private Vector3 OwnerPos(Vector3 effectDistances)
    {
        return curEffect.transform.position +
                    (owner.GetTransform().forward * effectDistances.z) +
                    (owner.GetTransform().right * effectDistances.x) +
                    (owner.GetTransform().up * effectDistances.y);
    }

    private Transform OwnerTransform(Vector3 effectDistances)
    {
        Transform temp = curEffect.transform;
        temp.position = curEffect.transform.position +
                    (owner.GetTransform().forward * effectDistances.z) +
                    (owner.GetTransform().right * effectDistances.x) +
                    (owner.GetTransform().up * effectDistances.y);

        return temp;
    }

    private Transform EffectTransform(Vector3 distance)
    {
        Transform temp = curEffect.transform;
        temp.position = curEffect.transform.position +
                    (curEffect.transform.forward * distance.z) +
                    (curEffect.transform.right * distance.x) +
                    (curEffect.transform.up * distance.y);

        return temp;
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
        isSetup = true;
        skillPower = data.skillPower;
        skillName = data.skillName;
        coolTime = data.coolTime;
    }

    public void ResetCoolTime()
    {
        timer = 0;
    }
}
