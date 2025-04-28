using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class ItemToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI type;
    [SerializeField] private TextMeshProUGUI grade;
    [SerializeField] private TextMeshProUGUI stat;
    [SerializeField] private TextMeshProUGUI description;

    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();

        if (rect == null)
            Debug.Log("1133");
    }

    public void SetupToolTip(ItemData data)
    {
        itemName.text = data.name;
        itemName.color = data.GetItemGradeColor();

        if(data is Use_ItemData)
        {
            type.text = "사용";
            stat.gameObject.SetActive(false);
            stat.text = "";
        }
        else
        {
            type.text = "장비";
            stat.gameObject.SetActive(true);
            Equipment_ItemData equip = data as Equipment_ItemData;
            stat.text = equip.GetStatDescription();
        }

        grade.text = data.itemGrade.ToString();
        grade.color = data.GetItemGradeColor();

        description.text = data.description;

        SetPos();
    }

    public void SetPos()
    {
        Vector3 mPos = Input.mousePosition;

        float xOffset = rect.rect.width/2;
        float yOffset = rect.rect.height/2;

        transform.position = mPos + new Vector3(xOffset,yOffset,0);
    }
}
