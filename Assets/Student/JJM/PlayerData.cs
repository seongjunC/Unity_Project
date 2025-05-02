using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int gold = 10000; // 플레이어의 초기 골드
    public List<ShopItem> inventory = new List<ShopItem>(); // 플레이어의 인벤토리

    public bool CanAfford(int price)
    {
        return gold >= price;
    }

    public void AddItem(ShopItem item)
    {
        inventory.Add(item);
    }

    public void RemoveItem(ShopItem item)
    {
        inventory.Remove(item);
    }

    public void AddGold(int amount)
    {
        gold += amount;
    }

    public void SubGold(int amount)
    {
        gold -= amount;
    }
}
