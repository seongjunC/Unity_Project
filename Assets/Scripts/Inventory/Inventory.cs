using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> inventoryDic = new Dictionary<ItemData, InventoryItem>();

    public event Action<int, ItemData> OnItemChanged;

    public void AddItem(int index ,ItemData data)
    {
        if (inventoryDic.TryGetValue(data, out InventoryItem item))
        {
            if (item.itemData is Equipment_ItemData)
            {
                InventoryItem newItem = new InventoryItem(data);
                inventory.Add(newItem);
            }
            else
                item.AddStack();

            OnItemChanged?.Invoke(index, data);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(data);
            inventory.Add(newItem);
            inventoryDic.Add(data, newItem);

            OnItemChanged?.Invoke(index, data);
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

            inventory.Remove(item);
            inventoryDic.Remove(data);

            OnItemChanged?.Invoke(index, null);
        }
    }
}
