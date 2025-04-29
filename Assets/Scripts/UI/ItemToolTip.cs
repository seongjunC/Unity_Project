using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class ItemToolTip : MonoBehaviour
{
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text type;
    [SerializeField] private TMP_Text grade;
    [SerializeField] private TMP_Text stat;
    [SerializeField] private TMP_Text description;
    [SerializeField] private float yPosOffset = 60;
    [SerializeField] private float xPosOffset = 60;

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
        
        float width = rect.rect.width;
        float height = rect.rect.height;

        Vector3 finalPos = mPos;

        if (mPos.x + width > Screen.width)
            finalPos.x = mPos.x - width + xPosOffset;
        else
            finalPos.x = mPos.x + width - xPosOffset;

        if(mPos.y + height > Screen.height)
            finalPos.y = mPos.y - height + yPosOffset;
        else
            finalPos.y = mPos.y + height - yPosOffset;

        rect.position = finalPos;
    }
}
