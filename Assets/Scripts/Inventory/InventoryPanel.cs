using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private ItemToolTip toolTip;
    public ItemSlot[] itemSlots;
    public EquipSlot equipSlot;
    public ItemData data;

    private void Awake()
    {
        Setup();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Manager.Data.inventory.AddItem(data);

        if (Input.GetKeyDown(KeyCode.Alpha5))
            UpdateAll();
    }

    private void Setup()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].slotNum = i;
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].ClearSlotUI();
        }

        equipSlot.ClearSlotUI();
    }

    private void OnEnable()
    {
        UpdateAll();
        Manager.Data.inventory.OnItemChanged += UpdateSlotUI;

        Manager.Data.equip.OnEquipmentChanged += UpdateAll;
        Manager.Data.equip.OnEquipmentChanged += UpdateEquipSlot;
    }


    private void OnDisable()
    {
        Manager.Data.inventory.OnItemChanged -= UpdateSlotUI;

        Manager.Data.equip.OnEquipmentChanged -= UpdateAll;
        Manager.Data.equip.OnEquipmentChanged -= UpdateEquipSlot;
    }
    private void UpdateAll()
    {
        Debug.Log("11");
        for (int i = 0; i < itemSlots.Length; i++)
        {
            Debug.Log("22223");
            itemSlots[i].UpdateSlot();
        }
    }

    public void UpdateSlotUI(int index, ItemData item)
    {
        if (itemSlots.Length < index) return;
            
        itemSlots[index].UpdateSlot(item);
    }

    public void UpdateEquipSlot()
    {
        equipSlot.UpdateSlot();
    }

    public void AddItem(ItemData data)
    {
        if (!CanAddItem()) return;

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

    public int? FindEmptySlotIndex()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].invItem == null || itemSlots[i].invItem.itemData == null)
                return i;
        }
        return null;
    }

    public void OpenToolTip(ItemData data)
    {
        toolTip.gameObject.SetActive(true);
        toolTip.SetupToolTip(data);
    }
    public void CloseToolTip() => toolTip.gameObject.SetActive(false);

    public bool CanAddItem()
    {
        return Manager.Data.inventory.TryGetEmptySlot();
    }
}
