using EnumType;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipItemData", menuName = "Data/ItemData/EquipmentData")]
public class Equipment_ItemData : ItemData
{
    public EquipmentType type;
    public int damage;
    public int hp;
    
    StringBuilder sb = new StringBuilder();

    public void AddStat()
    {
        Manager.Data.playerStatus.AddModifier(hp, StatType.MaxHP);
        Manager.Data.playerStatus.AddModifier(damage, StatType.Damage);
    }
    public void RemoveStat()
    {
        Manager.Data.playerStatus.RemoveModifier(hp, StatType.MaxHP);
        Manager.Data.playerStatus.RemoveModifier(damage, StatType.Damage);
    }

    public string GetStatDescription()
    {
        sb.Clear();

        sb.AppendLine(GetAddStatString(StatType.Damage, damage));
        sb.AppendLine(GetAddStatString(StatType.MaxHP, hp));

        return sb.ToString();
    }

    private string GetAddStatString(StatType type, int amount)
    {
        if (amount <= 0) return null;

        string name = (type) switch
        {
            StatType.MaxHP => "최대 체력",
            StatType.Damage => "공격력",
            _ => null
        };

        return $"+{name} : {amount}";
    }
}
