using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Shop UI")]
    public Transform itemListParent; // 아이템 목록을 표시할 부모 객체
    public GameObject shopItemPrefab; // 상점 아이템 UI 프리팹
    public Text playerGoldText; // 플레이어의 골드를 표시할 텍스트

    [Header("Shop Data")]
    public List<ShopItem> shopItems; // 상점에 등록된 아이템 리스트

    [Header("Player Data")]
    public PlayerData playerData; // 플레이어 데이터
    private void Start()
    {
        PopulateShop();
        UpdatePlayerGoldUI();
    }

    private void PopulateShop()
    {
        // 기존 UI 제거
        foreach (Transform child in itemListParent)
        {
            Destroy(child.gameObject);
        }

        // 상점 아이템 UI 생성
        foreach (var item in shopItems)
        {
            GameObject itemUI = Instantiate(shopItemPrefab, itemListParent);
            itemUI.GetComponentInChildren<Text>().text = $"{item.itemName}\nPrice: {item.price}";
            itemUI.GetComponentInChildren<Image>().sprite = item.icon;

            Button buyButton = itemUI.GetComponentInChildren<Button>();
            buyButton.onClick.AddListener(() => BuyItem(item));
        }
    }

    private void BuyItem(ShopItem item)
    {
        if (playerData.CanAfford(item.price))
        {
            playerData.SubGold(item.price);
            playerData.AddItem(item);
            Debug.Log($"아이템 구매: {item.itemName}, 가격: {item.price}");
            UpdatePlayerGoldUI();
        }
        else
        {
            Debug.LogWarning("골드가 부족합니다!");
        }
    }

    private void SellItem(ShopItem item)
    {
        if (playerData.inventory.Contains(item))
        {
            playerData.RemoveItem(item);
            playerData.AddGold(item.price / 2); // 판매 가격은 구매 가격의 절반으로 설정
            Debug.Log($"아이템 판매: {item.itemName}, 가격: {item.price / 2}");
            UpdatePlayerGoldUI();
        }
        else
        {
            Debug.LogWarning("인벤토리에 해당 아이템이 없습니다!");
        }
    }

    private void UpdatePlayerGoldUI()
    {
        playerGoldText.text = $"Gold: {playerData.gold}";
    }
}
