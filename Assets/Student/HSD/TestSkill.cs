using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestSkill", menuName = "Skill/Test")]
public class TestSkill : Skill
{
    public override IEnumerator SkillRoutine()
    {
        yield return waitDelay;

        CreateEffect(effectPrefab);

        DamageToTargets();
        yield return new WaitForSeconds(.3f);

        DamageToTargets();
        yield return new WaitForSeconds(.3f);

        DamageToTargets();
        yield return new WaitForSeconds(.3f);
    }

    protected override bool SkillCondition()
    {
        return true;
    }
}
