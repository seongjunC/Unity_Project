using UnityEngine;

public class BossMonster : Monster, ISkillOwner
{
    public bool isSkillActive { get; set; }

    public Transform GetTransform()
    {
        return transform;   
    }

    public int GetDamage()
    {
        return statusCon.status.damage;
    }
}
