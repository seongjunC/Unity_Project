using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDogam : MonoBehaviour
{
    public OpenDogam openDogam; // OpenDogam 스크립트를 참조

    public void CloseDogamUI()
    {
        if (openDogam != null)
        {
            // 도감 UI를 비활성화
            if (openDogam.dogamCanvas.gameObject.activeSelf)
            {
                openDogam.ToggleDogamUI();
            }
        }
        else
        {
            Debug.LogWarning("OpenDogam 스크립트가 설정되지 않았습니다.");
        }
    }
}
