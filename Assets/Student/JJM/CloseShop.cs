using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseShop : MonoBehaviour
{
    public ShopManager shopManager; // ShopManager 스크립트를 참조

    public void CloseShopUI()
    {
        if (shopManager != null)
        {
            // 상점 UI를 비활성화
            shopManager.CloseShop();
        }
        else
        {
            Debug.LogWarning("ShopManager가 설정되지 않았습니다.");
        }
    }
}

