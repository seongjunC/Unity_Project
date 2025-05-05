using EnumType;
using UnityEngine;

public class Skeleton_AnimTrigger : MonoBehaviour
{
    [SerializeField] private WeaponCollider weaponCollider;
    [SerializeField] private GameObject attackEffect;
    [SerializeField] private Transform[] attackEffectTransforms;

    private void WeaponColliderEnable() => weaponCollider.ColliderEnable();
    private void WeaponColliderDisable() => weaponCollider.ColliderDisable();

    private void CreateAttackEffect(int index)
    {
        GameObject curEffect = Manager.Resources.Instantiate(attackEffect, attackEffectTransforms[index].position, attackEffectTransforms[index].rotation, true);
        Manager.Resources.Destroy(curEffect, 2);
    }

    private void PlaySound(string name) => Manager.Audio.PlaySound(name, SoundType.Effect, Random.Range(0.8f, 1));

    private void PlayEffectAtPoint(string name) => Manager.Audio.PlayEffectAtPoint(name, transform.position, Random.Range(0.8f, 1));
    
}
