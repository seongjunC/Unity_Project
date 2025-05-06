using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class InteractUI : MonoBehaviour
{
    private ItemData itemData;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button button;
    [SerializeField] private ItemSlot slot;
    [SerializeField] private PlayableDirector director;

    public void SetupInteractUI(ItemData data)
    {
        itemData = data;
        slot.invItem.itemData = itemData;
        slot.UpdateSlot();

        if(Manager.Data.inventory.IsHaveItem(data))
        {
            text.text = "아이템을 가지고 있습니다.";
            button.interactable = true;
        }
        else
        {
            text.text = "아이템을 가지고 있지 않습니다.";
            button.interactable = false;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.Escape))
            gameObject.SetActive(false);
    }

    public void Open()
    {
        director.Play();    // 마지막 문 열고 나가는 컷씬 실행
    }
}
