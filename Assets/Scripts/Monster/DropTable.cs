using StructType;
using System.Collections.Generic;
using UnityEngine;

public class DropTable : MonoBehaviour
{
    [SerializeField] private List<DropData> drops = new List<DropData>();

    public void DropItem()
    {
        foreach (var drop in drops)
        {
            if(Random.Range(0, 100) <= drop.chance)
            {
                ItemObject newObject = Manager.Resources.Instantiate<GameObject>("ItemObject", transform.position, true).GetComponent<ItemObject>();
                newObject.SetupItemObejct(drop.item);
            }            
        }
    }
}
