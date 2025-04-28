using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    public ItemSlot[] itemSlots;
    public ItemData data;

    private void Awake()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].slotNum = i;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            AddItem(data);
    }

    private void OnEnable()
    {
        UpdateAll();
        Manager.Data.inventory.OnItemChanged += UpdateSlotUI;
    }


    private void OnDisable()
    {
        Manager.Data.inventory.OnItemChanged -= UpdateSlotUI;
    }
    private void UpdateAll()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].UpdateSlot();
        }
    }

    public void UpdateSlotUI(int index, ItemData item)
    {
        if (itemSlots.Length < index) return;
            
        itemSlots[index].UpdateSlot(item);
    }

    public void AddItem(ItemData data)
    {
        if (!CanAddItem()) return;

        int count = Manager.Data.inventory.inventory.Count;

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].invItem.itemData == null)
            {
                Manager.Data.inventory.AddItem(i, data);
                return;
            }
        }
    }

    public void RemoveItem(int index, ItemData data)
    {
        if (itemSlots[index].invItem.itemData != null)
        {
            Manager.Data.inventory.RemoveItem(index, data);
            itemSlots[index].invItem.itemData = null;
        }
    }

    public bool CanAddItem()
    {
        return Manager.Data.inventory.inventory.Count < itemSlots.Length;
    }
}
