using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Ultimate", menuName = "Skill/Ultimate")]
public class UltSkill : Skill
{
    public override IEnumerator SkillRoutine()
    {
        yield return null;
    }

    protected override bool SkillCondition()
    {
        return true;
    }
}
