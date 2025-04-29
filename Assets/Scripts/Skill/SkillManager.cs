using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Skill curSkill;
    public ISkillOnwer onwer;
    private Coroutine skillRoutine;

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
}
