using UnityEngine;

public class SkeletonBoss : Monster
{
    public SkeletonStateController stateCon;

    protected override void Awake()
    {
        base.Awake();
        stateCon = GetComponent<SkeletonStateController>();
    }
}
