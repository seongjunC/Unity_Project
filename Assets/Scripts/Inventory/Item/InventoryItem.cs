using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public ItemData itemData;

    public int stack;
    public InventoryItem(ItemData data)
    {
        itemData = data;
    }

    public void AddStack () => stack++;
    public void RemoveStack () => stack--;
}
