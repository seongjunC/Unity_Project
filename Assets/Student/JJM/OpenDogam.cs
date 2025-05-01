using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenDogam : MonoBehaviour
{
    [Header("Dogam UI")]
    public Canvas dogamCanvas; // ?꾧컧 UI濡??ъ슜??罹붾쾭??
    public GameObject itemUIPrefab; // ?꾩씠??UI ?꾨━??(?꾩씠???대쫫, ?꾩씠肄??쒖떆)
    public Transform itemListParent; // ?꾩씠??紐⑸줉???쒖떆??遺紐?媛앹껜
    public Text itemDetailText; // ?꾩씠???곸꽭 ?뺣낫瑜??쒖떆???띿뒪??
    public Image itemDetailIcon; // ?꾩씠???곸꽭 ?뺣낫???꾩씠肄??쒖떆

    [Header("Item Data")]
    public List<ItemData> itemDatabase; // 아이템 데이터베이스
    private List<ItemData> originalOrder; // 초기 순서를 저장할 리스트

    [Header("Search")]
    public TMP_InputField searchInputField; // 검색 입력 필드

    [Header("Sorting")]
    public Dropdown sortDropdown; // 정렬 기준 선택 Dropdown
    private void Start()
    {
        // 초기 순서 저장
        originalOrder = new List<ItemData>(itemDatabase);

        PopulateDogam();
        // 검색 입력 필드 값 변경 시 검색 메서드 호출
        searchInputField.onValueChanged.AddListener(OnSearchValueChanged);
        // Dropdown 값 변경 시 정렬 메서드 호출
        sortDropdown.onValueChanged.AddListener(OnSortChanged);
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
            Debug.Log($"Dogam Canvas ?곹깭: {dogamCanvas.gameObject.activeSelf}");
        }
        else
        {
            Debug.LogWarning("Dogam Canvas媛 ?ㅼ젙?섏? ?딆븯?듬땲??");
        }
    }

    private void PopulateDogam()
    {
        // 기존 UI 제거
        foreach (Transform child in itemListParent)
        {
            Destroy(child.gameObject);
        }

        // 아이템 목록 생성
        foreach (var item in itemDatabase)
        {
            if (item == null)
            {
                Debug.LogError("itemDatabase??null ??ぉ???덉뒿?덈떎.");
                continue;
            }

            Debug.Log($"?꾩씠??異붽?: {item.itemName}");

            GameObject itemUI = Instantiate(itemUIPrefab, itemListParent);
            if (itemUI == null)
            {
                Debug.LogError("itemUIPrefab?먯꽌 ?앹꽦??itemUI媛 null?낅땲??");
                continue;
            }

            Text textComponent = itemUI.GetComponentInChildren<Text>();
            if (textComponent == null)
            {
                Debug.LogError("itemUIPrefab??Text 而댄룷?뚰듃媛 ?놁뒿?덈떎.");
                continue;
            }
            textComponent.text = item.itemName;

            Image imageComponent = itemUI.GetComponentInChildren<Image>();
            if (imageComponent == null)
            {
                Debug.LogError("itemUIPrefab??Image 而댄룷?뚰듃媛 ?놁뒿?덈떎.");
                continue;
            }
            imageComponent.sprite = item.icon;

            Button itemButton = itemUI.GetComponent<Button>();
            if (itemButton == null)
            {
                Debug.LogError("itemUIPrefab??Button 而댄룷?뚰듃媛 ?놁뒿?덈떎.");
                continue;
            }

            Debug.Log($"Button 而댄룷?뚰듃媛 ?ㅼ젙?섏뿀?듬땲?? {item.itemName}");

            itemButton.onClick.AddListener(() => ShowItemDetails(item));
        }
    }

    private void ShowItemDetails(ItemData item)
    {
        Debug.Log($"?꾩씠???곸꽭 ?뺣낫 ?쒖떆: {item.itemName}");
        // ?꾩씠???곸꽭 ?뺣낫 ?쒖떆
        itemDetailText.text = $"Name: {item.itemName}\n" +
                              $"Grade: {item.itemGrade}\n" +
                              $"Description: {item.description}";
        itemDetailIcon.sprite = item.icon;
        itemDetailIcon.color = item.GetItemGradeColor(); // ?깃툒???곕Ⅸ ?됱긽 ?ㅼ젙
    }
    private void OnSearchValueChanged(string searchText)
    {
        // 검색어가 비어 있으면 전체 목록 표시
        if (string.IsNullOrEmpty(searchText))
        {
            PopulateDogam();
            return;
        }

        // 기존 UI 제거
        foreach (Transform child in itemListParent)
        {
            Destroy(child.gameObject);
        }

        // 검색 결과에 해당하는 아이템만 표시
        var searchResults = itemDatabase.Where(item => item.itemName.ToLower().Contains(searchText.ToLower())).ToList();
        foreach (var item in searchResults)
        {
            GameObject itemUI = Instantiate(itemUIPrefab, itemListParent);
            itemUI.GetComponentInChildren<Text>().text = item.itemName;
            itemUI.GetComponentInChildren<Image>().sprite = item.icon;

            Button itemButton = itemUI.GetComponent<Button>();
            itemButton.onClick.AddListener(() => ShowItemDetails(item));
        }
    }
    private void OnSortChanged(int selectedIndex)
    {
        switch (selectedIndex)
        {
            case 0: // 기본 순서
                itemDatabase = new List<ItemData>(originalOrder);
                break;
            case 1: // 이름순 정렬
                itemDatabase = itemDatabase.OrderBy(item => item.itemName).ToList();
                break;
            case 2: // 등급순 정렬
                itemDatabase = itemDatabase.OrderBy(item => item.itemGrade).ToList();
                break;
            default:
                Debug.LogWarning("알 수 없는 정렬 기준입니다.");
                return;
        }

        Debug.Log($"정렬 기준 변경: {sortDropdown.options[selectedIndex].text}");
        PopulateDogam(); // 정렬된 데이터로 UI 갱신
    }
}
