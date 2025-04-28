using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UseItemData", menuName = "Data/ItemData/UseData")]
public class Use_ItemData : ItemData, IUsable
{
    [SerializeField] private UseEffect useEffect;

    public virtual void Use()
    {
        useEffect.Excute();
    }
}
