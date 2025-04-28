using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite icon;
    [SerializeField] private TextMeshProUGUI stackSize;
    [SerializeField] private InGame_UI UI;

    public InventoryItem invItem;
    public int slotNum;

    public void UpdateSlot(ItemData item)
    {
        invItem.itemData = item;

        if (invItem.itemData != null)
        {
            icon = invItem.itemData.icon;
            stackSize.text = invItem.stack.ToString();
        }
        else
        {
            icon = null;
            stackSize.text = "";
        }
    }

    public void UpdateSlot()
    {
        if (invItem.itemData != null)
        {
            icon = invItem.itemData.icon;
            stackSize.text = invItem.stack.ToString();
        }
        else
        {
            icon = null;
            stackSize.text = "";
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (invItem.itemData == null) return;
        UI.OpenToolTip(invItem.itemData);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (invItem.itemData == null) return;
        UI.CloseToolTip();   
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

}
