using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractor : MonoBehaviour
{
    public ShopManager shopManager; // 상점 매니저 참조

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("NPC와 상호작용 가능");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E)) // E 키로 상호작용
        {
            Debug.Log("NPC와 상호작용 중");
            shopManager.OpenShop(); // 상점 열기
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("NPC와 상호작용 종료");
        }
    }
}
