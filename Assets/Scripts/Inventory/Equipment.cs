using System;
using UnityEngine;
using static UnityEditor.Progress;

public class Equipment : MonoBehaviour
{
    public Action OnEquipmentChanged;

    public void EquipItem(int index, Equipment_ItemData item)
    {
        if (item == null) return;

        if (!Manager.Data.inventory.TryGetEmptySlot()) return;

        InventoryItem invItem = new InventoryItem(item);
        Inventory inv = Manager.Data.inventory;

        Equipment_ItemData oldItem = null;

        if (inv.equipment != null)
        {
            oldItem = inv.equipment.itemData as Equipment_ItemData;
        }

        inv.RemoveItem(index, item);

        if (oldItem != null)
        {
            UnEquipItem(index, oldItem);
        }

        inv.equipment = invItem;
        item.AddStat();
        inv.AddEquipmentDic(item, invItem);

        OnEquipmentChanged?.Invoke();
    }

    public void UnEquipItem(int index, Equipment_ItemData equip)
    {
        if (equip == null) return;

        if (!Manager.Data.inventory.TryGetEmptySlot()) return;

        Manager.Data.inventory.AddItem(index, equip);
        equip.RemoveStat();
        Manager.Data.inventory.equipment = null;
        Manager.Data.inventory.RemoveEquipmentDic(equip);

        OnEquipmentChanged?.Invoke();
    }
}
