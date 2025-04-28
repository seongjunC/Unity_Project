using EnumType;
using StructType;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Data/ItemData")]
public class ItemData : ScriptableObject
{
    private Dictionary<ItemGrade, Color> itemGradeColor = new Dictionary<ItemGrade, Color>()
    {
        { ItemGrade.Common, Color.gray },
        { ItemGrade.UnCommon, Color.white },
        { ItemGrade.Rare, Color.cyan },
        { ItemGrade.Epic, Color.magenta },
        { ItemGrade.Unique, Color.yellow },
        { ItemGrade.Legendary, Color.red },
    };
        
    public ItemGrade itemGrade;
    public Sprite icon;
    public string itemName;

    [TextArea]
    public string description;
    public itemMeshData itemMeshData;

    public Color GetItemGradeColor()
    {
        return itemGradeColor[itemGrade];
    }
}
