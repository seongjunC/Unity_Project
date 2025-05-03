using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectTrigger : MonoBehaviour
{
    private ItemObject itemObject => GetComponentInParent<ItemObject>();

    private void Start()
    {
        Invoke("ColliderActive", 1f);
    }

    private void ColliderActive() => GetComponent<SphereCollider>().enabled = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collector"))
        {
            itemObject.PickupItem();
        }
    }
}
