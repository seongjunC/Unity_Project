using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDogam : MonoBehaviour
{
    [Header("Dogam UI")]
    public Canvas dogamCanvas; // 도감 UI로 사용할 캔버스
    public GameObject itemUIPrefab; // 아이템 UI 프리팹 (아이템 이름, 아이콘 표시)
    public Transform itemListParent; // 아이템 목록을 표시할 부모 객체
    public Text itemDetailText; // 아이템 상세 정보를 표시할 텍스트
    public Image itemDetailIcon; // 아이템 상세 정보의 아이콘 표시

    [Header("Item Data")]
    public List<ItemData> itemDatabase; // 아이템 데이터베이스

    private void Start()
    {
        PopulateDogam();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ToggleDogamUI();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (dogamCanvas.gameObject.activeSelf)
            {
                ToggleDogamUI();
            }
        }
    }

    public void ToggleDogamUI()
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

    private void PopulateDogam()
    {
        if (itemDatabase == null || itemDatabase.Count == 0)
        {
            Debug.LogError("itemDatabase가 비어 있습니다. 아이템 데이터를 추가하세요.");
            return;
        }

        foreach (var item in itemDatabase)
        {
            if (item == null)
            {
                Debug.LogError("itemDatabase에 null 항목이 있습니다.");
                continue;
            }

            Debug.Log($"아이템 추가: {item.itemName}");

            GameObject itemUI = Instantiate(itemUIPrefab, itemListParent);
            if (itemUI == null)
            {
                Debug.LogError("itemUIPrefab에서 생성된 itemUI가 null입니다.");
                continue;
            }

            Text textComponent = itemUI.GetComponentInChildren<Text>();
            if (textComponent == null)
            {
                Debug.LogError("itemUIPrefab에 Text 컴포넌트가 없습니다.");
                continue;
            }
            textComponent.text = item.itemName;

            Image imageComponent = itemUI.GetComponentInChildren<Image>();
            if (imageComponent == null)
            {
                Debug.LogError("itemUIPrefab에 Image 컴포넌트가 없습니다.");
                continue;
            }
            imageComponent.sprite = item.icon;

            Button itemButton = itemUI.GetComponent<Button>();
            if (itemButton == null)
            {
                Debug.LogError("itemUIPrefab에 Button 컴포넌트가 없습니다.");
                continue;
            }
            Debug.Log($"Button 컴포넌트가 설정되었습니다: {item.itemName}");
            itemButton.onClick.AddListener(() => ShowItemDetails(item));
        }
    }

    private void ShowItemDetails(ItemData item)
    {
        Debug.Log($"아이템 상세 정보 표시: {item.itemName}");
        // 아이템 상세 정보 표시
        itemDetailText.text = $"Name: {item.itemName}\n" +
                              $"Grade: {item.itemGrade}\n" +
                              $"Description: {item.description}";
        itemDetailIcon.sprite = item.icon;
        itemDetailIcon.color = item.GetItemGradeColor(); // 등급에 따른 색상 설정
    }
}
