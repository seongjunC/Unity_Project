using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Shop UI")]
    public Transform itemListParent; // 아이템 목록을 표시할 부모 객체
    public GameObject shopItemPrefab; // 상점 아이템 UI 프리팹

    [Header("Shop Data")]
    public List<ShopItem> shopItems; // 상점에 등록된 아이템 리스트

    private void Start()
    {
        PopulateShop();
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
        Debug.Log($"아이템 구매: {item.itemName}, 가격: {item.price}");
        // 구매 로직 추가 (예: 플레이어 골드 차감, 아이템 추가 등)
    }
}
