using EnumType;
using System.Collections;
using UnityEngine;

public class PlayerAnimTrigger : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private LayerMask target;
    private GameObject effect;
    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Attack()
    {
        Collider[] cols = Physics.OverlapSphere(player.attackTransform.position, player.attackRaius, target);

        foreach (var c in cols)
        {
            if (c.CompareTag("Monster"))
            {
                if (c.TryGetComponent<IDamagable>(out IDamagable damagable))
                {
                    damagable.TakeDamage(player.attackForce[player.stateCon.attackState.comboCount - 1]);
                }
            }
        }
    }

    private void AttackEffect(int count)
    {
        Transform t = player.attackEffectTransform[count - 1];

        if(player.curWeapon != null)
        {
            if(count == 3)
            {
                effect = Manager.Resources.Instantiate(player.curWeapon.lastEffect, t.position, t.rotation, true);
            }
            else
                effect = Manager.Resources.Instantiate(player.curWeapon.effect, t.position, t.rotation, true);
        }
        else
        {
            if (count == 3)
                effect = Manager.Resources.Instantiate(player.lastAttackEffect, t.position, t.rotation, true);
            else
                effect = Manager.Resources.Instantiate(player.defaultAttackEffect, t.position, t.rotation, true);
        }

        Manager.Resources.Destroy(effect, 3);
    }

    private void SlowMotion(float scale, float duration) => Manager.Game.SlowMotion(scale, duration);
    private void SlowMotionHalf(float duration) => Manager.Game.SlowMotion(.5f, duration);
    private void SoundEffect(string name) => Manager.Audio.PlaySound(name, SoundType.Effect, Random.Range(0.7f, 1)); 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(player.attackTransform.position, player.attackRaius);
    }
}