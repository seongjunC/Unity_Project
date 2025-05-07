using EnumType;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipItemData", menuName = "Data/ItemData/EquipmentData")]
public class Equipment_ItemData : ItemData
{
    public EquipmentType type;
    public int damage;
    public int maxHP;
    public int critChance;
    public int critDamage;

    public GameObject effect;
    public GameObject lastEffect;

    StringBuilder sb = new StringBuilder();

    public void AddStat()
    {
        Manager.Data.playerStatus.damage.AddModifier(damage);
        Manager.Data.playerStatus.maxHP.AddModifier(maxHP);
        Manager.Data.playerStatus.critChance.AddModifier(critChance);
        Manager.Data.playerStatus.critDamage.AddModifier(critDamage);
    }

    public void RemoveStat()
    {
        Manager.Data.playerStatus.damage.RemoveModifier(damage);
        Manager.Data.playerStatus.maxHP.RemoveModifier(maxHP);
        Manager.Data.playerStatus.critChance.RemoveModifier(critChance);
        Manager.Data.playerStatus.critDamage.RemoveModifier(critDamage);
    }

    public string GetStatDescription()
    {
        sb.Clear();

        sb.AppendLine(GetAddStatString("공격력", damage));
        sb.AppendLine(GetAddStatString("최대 체력", maxHP));
        sb.AppendLine(GetAddStatString("크리티컬 확률", critChance));
        sb.AppendLine(GetAddStatString("크리티컬 데미지", critDamage));

        return sb.ToString();
    }

    private string GetAddStatString(string name, int amount)
    {
        if (amount <= 0) return null;

        return $"+{name} : {amount}";
    }
}
