using EnumType;
using StructType;
using System;
using UnityEngine;

[Serializable]
public class SkillOverlapData
{
    public OverlapType overlapType;
    public LayerMask targetMask;

    [Header("Box")]
    public Vector3 boxSize;

    [Header("Capsule")]
    public float height;
    private Vector3 offset;

    [Header("Overlap Data")]
    public float radius;
    public Vector3 distance;
    
    private Collider[] GetTarget (Transform effectTransform, Vector3 distance, float radius, SkillVector skillVec)
    {
        Vector3 forward = skillVec.forward;
        Vector3 right = skillVec.right;
        Vector3 up = skillVec.up;

        Vector3 position = effectTransform.position + (forward * distance.z) + (right * distance.x) + (up * distance.y);

        if (overlapType == OverlapType.Capsule)
        {
            offset = effectTransform.forward * (height / 2 - radius);
        }

        return (overlapType) switch
        {
            OverlapType.Sphere => Physics.OverlapSphere(position, radius, targetMask),
            OverlapType.Capsule => Physics.OverlapCapsule(position + offset, position - offset, height, targetMask),
            OverlapType.Box => Physics.OverlapBox(position, boxSize * radius, effectTransform.rotation, targetMask),
            _ => null
        };
    }

    public void DealDamageToTargets(Transform curEffect, SkillVector skillVec, float skillPower, int damage)
    {
        foreach (var hit in GetTarget(curEffect.transform, distance, radius, skillVec))
        {
            if (hit.TryGetComponent<IDamagable>(out IDamagable target))
                target.TakeDamage((int)(damage * skillPower));
        }
    }
}
