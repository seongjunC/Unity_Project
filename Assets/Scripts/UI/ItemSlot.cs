using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI stackSize;
    [SerializeField] protected InventoryPanel invPanel;

    [SerializeField] protected InventoryItem _invItem;
    public virtual InventoryItem invItem
    {
        get => _invItem; set { _invItem = value; }
    }

    public int slotNum;

    private void Awake()
    {
        _invItem = new InventoryItem(null);
    }

    public void UpdateSlot(ItemData item)
    {
        invItem.itemData = item;

        if (invItem != null && invItem.itemData != null)
        {
            icon.sprite = invItem.itemData.icon;
            icon.color = Color.white;

            stackSize.text = invItem.stack.ToString();

            if(invItem.itemData is Equipment_ItemData)
            {
                stackSize.gameObject.SetActive(false);
            }
            else
                stackSize.gameObject.SetActive(true);
        }
        else
        {
            ClearSlotUI();
        }
    }

    public void ClearSlotUI()
    {
        icon.sprite = null;
        stackSize.text = "";

        icon.color = Color.clear;
    }

    public void UpdateSlot()
    {
        if (invItem != null && invItem.itemData != null)
        {
            icon.sprite = invItem.itemData.icon;
            icon.color = Color.white;
            stackSize.text = invItem.stack.ToString();
        }
        else
        {
            ClearSlotUI();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (invItem == null || invItem.itemData == null) return;

        invPanel.OpenToolTip(invItem.itemData);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (invItem == null || invItem.itemData == null) return;

        invPanel.CloseToolTip();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (invItem == null || invItem.itemData == null) return;

        if(invItem.itemData is Equipment_ItemData)
        {
            Manager.Data.equip.EquipItem(slotNum, invItem.itemData as Equipment_ItemData);
        }
        else if(invItem.itemData is Use_ItemData item)
        {
            item.Use();
            Manager.Data.inventory.RemoveItem(slotNum, invItem.itemData);
        }

        invPanel.CloseToolTip();
    }

}
