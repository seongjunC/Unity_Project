using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventoryItem[] inventory = new InventoryItem[30];
    private Dictionary<ItemData, InventoryItem> inventoryDic = new Dictionary<ItemData, InventoryItem>();

    public InventoryItem equipment;
    private Dictionary<Equipment_ItemData, InventoryItem> equipmentDic = new Dictionary<Equipment_ItemData, InventoryItem>();

    public Action<int, ItemData> OnItemChanged;

    public void AddItem(int index ,ItemData data)
    {
        if (inventoryDic.TryGetValue(data, out InventoryItem item))
        {
            if (item.itemData is Equipment_ItemData)
            {
                InventoryItem newItem = new InventoryItem(data);
                inventory[index] = newItem;
            }
            else
                item.AddStack();

            OnItemChanged?.Invoke(index, data);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(data);
            inventory[index] = newItem;
            inventoryDic.Add(data, newItem);

            OnItemChanged?.Invoke(index, data);
        }
    }

    public void AddItem(ItemData data)
    {
        int index = 0;
        if (data == null)
            Debug.Log("Data is Null");

        if (inventoryDic.TryGetValue(data, out InventoryItem item))
        {
            if (item == null)
                Debug.Log("Item is Null");

            if (item.itemData is Equipment_ItemData)
            {
                if(TryGetEmptySlotIndex(out index))
                {
                    InventoryItem newItem = new InventoryItem(data);
                    inventory[index] = newItem;
                }                
            }
            else
                item.AddStack();

            OnItemChanged?.Invoke(index, data);
        }
        else
        {
            if(TryGetEmptySlotIndex(out index))
            {
                InventoryItem newItem = new InventoryItem(data);
                inventory[index] = newItem;
                inventoryDic.Add(data, newItem);

                OnItemChanged?.Invoke(index, data);
            }
        }
    }

    public void RemoveItem(int index, ItemData data)
    {
        if (inventoryDic.TryGetValue(data, out InventoryItem item))
        {
            if (item.stack > 1)
            {
                item.RemoveStack();
                OnItemChanged?.Invoke(index, data);
                return;
            }

            inventory[index] = null;
            inventoryDic.Remove(data);

            OnItemChanged?.Invoke(index, null);
        }
    }

    public bool IsHaveItem(ItemData data)
    {
        return inventoryDic[data] != null;
    }
    public bool TryGetEmptySlotIndex(out int index)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null || inventory[i].itemData == null)
            {
                index = i;
                return true;
            }
        }

        index = -1;
        return false;
    }

    public bool TryGetEmptySlot()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null || inventory[i].itemData == null)
            {
                return true;
            }
        }
        return false;
    }

    public Equipment_ItemData GetCurrentWeapon()
    {
        foreach (var item in equipmentDic.Keys)
        {
            return item;
        }
        return null;
    }

    public void AddEquipmentDic(Equipment_ItemData equip, InventoryItem item)
    {
        equipmentDic.Add(equip, item);
    }
    public void RemoveEquipmentDic(Equipment_ItemData equip)
    {
        equipmentDic.Remove(equip);
    }
}
