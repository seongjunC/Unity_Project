using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageCalulator
{
    public static void PlayerAttackCalculator(PlayerStatusData player, MonsterStatusController monster, float attackPower)
    {
        float totalDamage = player.damage.GetValue();
        bool isCrit = false;

        if (player.critChance.GetValue() > Random.Range(0, 100))
        {
            totalDamage *= player.critDamage.GetValue() / 100f;

            isCrit = true;
        }

        totalDamage *= attackPower;

        monster.DcreaseHealth(Mathf.RoundToInt(totalDamage), isCrit);
    }
}
