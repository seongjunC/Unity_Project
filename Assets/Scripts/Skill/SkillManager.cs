using EnumType;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Skill curSkill;
    public ISkillOnwer onwer;
    private Coroutine skillRoutine;
    [SerializeField] private float test;

    private void Awake()
    {
        onwer = GetComponent<ISkillOnwer>();
    }

    public void UseSKill(Skill skill)
    {
        if(skillRoutine == null && skill.CanUse())
        {
            curSkill = skill;
            curSkill.Init(onwer);
            StartCoroutine(curSkill.CoolTimeRoutine());
            StartCoroutine(SkillMainRoutine());
        }
    }

    public void CancelSkill()
    {
        if (skillRoutine == null) return;

        StopCoroutine(skillRoutine);
        curSkill.DestroyEffect();
        skillRoutine = null;
        curSkill = null;
    }

    private IEnumerator SkillMainRoutine()
    {
        curSkill.StartSkill();
        skillRoutine ??= StartCoroutine(curSkill.SkillRoutine());
        yield return curSkill.waitSkillEndDelay;
        curSkill.EndSkill();
        skillRoutine = null;
    }

#if UNITY_EDITOR

    [SerializeField] private BoxCollider box;
    [SerializeField] private CapsuleCollider capsule;
    [SerializeField] private SphereCollider sphere;
    private void OnValidate()
    {
        if (curSkill.overlap.overlapType == OverlapType.Capsule)
        {
            capsule.gameObject.SetActive(true);
            box.gameObject.SetActive(false);
            sphere.gameObject.SetActive(false);
            capsule.center = new Vector3(0, 0.5f, 0) + curSkill.overlap.distance;
            capsule.height = curSkill.overlap.height;
            capsule.radius = curSkill.overlap.radius;
        }
        else if (curSkill.overlap.overlapType == OverlapType.Box)
        {
            capsule.gameObject.SetActive(false);
            box.gameObject.SetActive(true);
            sphere.gameObject.SetActive(false);
            box.center = new Vector3(0, 0.5f, 0) + curSkill.overlap.distance;
            box.size = curSkill.overlap.boxSize * curSkill.overlap.radius;
        }
        else
        {
            capsule.gameObject.SetActive(false);
            box.gameObject.SetActive(false);
            sphere.gameObject.SetActive(true);
            sphere.center = new Vector3(0, 0.5f, 0) + curSkill.overlap.distance;
            sphere.radius = curSkill.overlap.radius;
        }
    }
#endif
}
