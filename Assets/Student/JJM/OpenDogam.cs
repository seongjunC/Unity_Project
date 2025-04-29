using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Dogam UI")]
    public Canvas dogamCanvas; // 도감 UI로 사용할 캔버스

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ToggleDogamUI();
        }
    }

    private void ToggleDogamUI()
    {
        if (dogamCanvas != null)
        {
            dogamCanvas.gameObject.SetActive(!dogamCanvas.gameObject.activeSelf);
            Debug.Log($"Dogam Canvas 상태: {dogamCanvas.gameObject.activeSelf}");
        }
        else
        {
            Debug.LogWarning("Dogam Canvas가 설정되지 않았습니다.");
        }
    }
}
