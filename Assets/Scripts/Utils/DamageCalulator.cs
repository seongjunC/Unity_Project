using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageCalulator
{
    public static void PlayerAttackCalculator(PlayerStatusData player, MonsterStatusController monster, float attackPower)
    {
        float totalDamage = player.damage.GetValue();

        if (player.critChance.GetValue() > Random.Range(0, 100))
        {
            totalDamage *= (player.critDamage.GetValue() / 100);
        }

        totalDamage *= attackPower;

        monster.DcreaseHealth((int)totalDamage);
    }
}
