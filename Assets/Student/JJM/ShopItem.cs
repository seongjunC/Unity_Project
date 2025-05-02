using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewShopItem", menuName = "Shop/ShopItem")]
public class ShopItem : ScriptableObject
{
    public string itemName; // 아이템 이름
    public Sprite icon; // 아이템 아이콘
    public int price; // 아이템 가격
    public string description; // 아이템 설명
}
