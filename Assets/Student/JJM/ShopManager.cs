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

    [Header("Purchase Popup")]
    public GameObject purchasePopup; // 구매 확인 팝업
    public Text popupMessageText; // 팝업 메시지 텍스트
    public Button confirmButton; // 확인 버튼
    public Button cancelButton; // 취소 버튼

    private ShopItem selectedItem; // 현재 선택된 아이템
    private void Start()
    {
        PopulateShop();
        UpdatePlayerGoldUI();

        // 팝업 버튼 이벤트 설정
        confirmButton.onClick.AddListener(ConfirmPurchase);
        cancelButton.onClick.AddListener(ClosePopup);
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
            buyButton.onClick.AddListener(() => ShowPurchasePopup(item));
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
    private void ShowPurchasePopup(ShopItem item)
    {
        selectedItem = item; // 선택된 아이템 저장
        popupMessageText.text = $"'{item.itemName}'을(를) {item.price} 골드에 구매하시겠습니까?";
        purchasePopup.SetActive(true); // 팝업 활성화
    }

    private void ConfirmPurchase()
    {
        if (selectedItem != null && playerData.CanAfford(selectedItem.price))
        {
            playerData.SubGold(selectedItem.price);
            playerData.AddItem(selectedItem);
            Debug.Log($"아이템 구매: {selectedItem.itemName}, 가격: {selectedItem.price}");
            UpdatePlayerGoldUI();
        }
        else
        {
            Debug.LogWarning("골드가 부족합니다!");
        }

        ClosePopup(); // 팝업 닫기
    }

    private void ClosePopup()
    {
        purchasePopup.SetActive(false); // 팝업 비활성화
        selectedItem = null; // 선택된 아이템 초기화
    }
    private void UpdatePlayerGoldUI()
    {
        playerGoldText.text = $"Gold: {playerData.gold}";
    }
}
