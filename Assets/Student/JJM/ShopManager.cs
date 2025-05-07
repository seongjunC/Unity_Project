using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Shop UI")]
    public GameObject shopUI; // 상점 UI 패널
    public Transform itemListParent; // 아이템 목록을 표시할 부모 객체
    public GameObject shopItemPrefab; // 상점 아이템 UI 프리팹
    public Text playerGoldText; // 플레이어의 골드를 표시할 텍스트

    [Header("Shop Data")]
    public List<ItemData> shopItems; // 상점에 등록된 아이템 리스트

    [Header("Player Data")]
   // public PlayerData playerData; // 플레이어 데이터
    public Inventory playerInventory; // 플레이어 인벤토리

    [Header("Purchase Popup")]
    public GameObject purchasePopup; // 구매 확인 팝업
    public Text popupMessageText; // 팝업 메시지 텍스트
    public Button confirmButton; // 확인 버튼
    public Button cancelButton; // 취소 버튼

    [Header("Failure Message")]
    public GameObject failureMessage; // 구매 실패 메시지 UI
    public float failureMessageDuration = 2f; // 실패 메시지 표시 시간

    private ItemData selectedItem; // 현재 선택된 아이템
    private void Start()
    {
        PopulateShop();
        UpdatePlayerGoldUI();

        // 팝업 버튼 이벤트 설정
        confirmButton.onClick.AddListener(ConfirmPurchase);
        cancelButton.onClick.AddListener(ClosePopup);

        
    }
    
    public void OpenShop()
    {
        shopUI.SetActive(true); // 상점 UI 활성화
        Debug.Log("상점 열림");
    }

    public void CloseShop()
    {
        shopUI.SetActive(false); // 상점 UI 비활성화
        Debug.Log("상점 닫힘");
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

    
    private void ShowPurchasePopup(ItemData item)
    {
        selectedItem = item; // 선택된 아이템 저장
        popupMessageText.text = $"'{item.itemName}'을(를) {item.price} 골드에 구매하시겠습니까?";
        purchasePopup.SetActive(true); // 팝업 활성화
    }

    private void ConfirmPurchase()
    {
        if (selectedItem != null && GameManager.GetInstance().gold >= selectedItem.price)
        {
            // 골드 차감
            GameManager.GetInstance().RemoveGold(selectedItem.price);

            // 인벤토리에 아이템 추가
            if (playerInventory.TryGetEmptySlotIndex(out int index))
            {
                playerInventory.AddItem(index, selectedItem);
                Debug.Log($"아이템 구매: {selectedItem.itemName}");
            }
            else
            {
                ShowFailureMessage("인벤토리에 빈 공간이 없습니다!");
            }
        }
        else
        {
            ShowFailureMessage("골드가 부족합니다!");
        }

        ClosePopup(); // 팝업 닫기
    }
    private void ShowFailureMessage(string message)
    {
        if (failureMessage != null)
        {
            Text failureText = failureMessage.GetComponent<Text>();
            if (failureText != null)
            {
                failureText.text = message;
            }

            failureMessage.SetActive(true); // 실패 메시지 활성화
            StartCoroutine(HideFailureMessageAfterDelay());
        }
    }

    private IEnumerator HideFailureMessageAfterDelay()
    {
        yield return new WaitForSeconds(failureMessageDuration);
        failureMessage.SetActive(false); // 실패 메시지 비활성화
    }
    private void ClosePopup()
    {
        purchasePopup.SetActive(false); // 팝업 비활성화
        selectedItem = null; // 선택된 아이템 초기화
    }
    private void UpdatePlayerGoldUI()
    {
        playerGoldText.text = $"Gold: {GameManager.GetInstance().gold}";
    }
}
