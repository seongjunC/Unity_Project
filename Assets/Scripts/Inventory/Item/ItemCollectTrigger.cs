using UnityEngine;

public class ItemCollectTrigger : MonoBehaviour
{
    private ItemObject itemObject;

    private void Awake()
    {
        itemObject = GetComponentInParent<ItemObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            itemObject.PickupItem();
        }
    }
}
