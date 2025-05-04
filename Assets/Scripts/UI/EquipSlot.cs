using UnityEngine.EventSystems;

public class EquipSlot : ItemSlot
{
    public override InventoryItem invItem { get => Manager.Data.inventory.equipment; set => base.invItem = value; }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (invItem == null || invItem.itemData == null) return;

        int? index = invPanel.FindEmptySlotIndex();
        if (index == null) return;

        Manager.Data.equip.UnEquipItem(index.Value, invItem.itemData as Equipment_ItemData);
        ClearSlotUI();
        invPanel.CloseToolTip();
    }


}
