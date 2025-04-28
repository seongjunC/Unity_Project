using EnumType;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipItemData", menuName = "Data/ItemData/EquipmentData")]
public class Equipment_ItemData : ItemData
{
    public EquipmentType type;
    public int damage;
    public int hp;

    public void AddStat()
    {
        Manager.Data.playerStatus.AddHP(hp);
        Manager.Data.playerStatus.AddDamage(damage);
    }
    public void RemoveStat()
    {
        Manager.Data.playerStatus.AddHP(-hp);
        Manager.Data.playerStatus.AddDamage(-damage);
    }
}
