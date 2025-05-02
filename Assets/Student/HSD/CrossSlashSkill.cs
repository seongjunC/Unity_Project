using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "CrossSlashSkill", menuName = "Skill/CrossSlash")]
public class CrossSlashSkill : Skill
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
