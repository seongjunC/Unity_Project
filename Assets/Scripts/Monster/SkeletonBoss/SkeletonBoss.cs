using System;
using UnityEngine;

public class SkeletonBoss : Monster, ISkillOwner
{
    public SkeletonStateController stateCon {  get; private set; }
    public SkeletonSkillController skillCon { get; private set; }

    public float attackDistance;
    public bool isSkillActive { get; set; }
    public bool isStun;
    public bool isAttack;


    public int GetDamage()
    {
        return statusCon.status.damage;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    protected override void Awake()
    {
        base.Awake();
        stateCon = GetComponent<SkeletonStateController>();
        skillCon = GetComponent<SkeletonSkillController>();
    }
}
