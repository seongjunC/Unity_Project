using StructType;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffEffect", menuName = "Data/ItemData/Effect/Buff")]
public class BuffEffect : UseEffect
{
    [SerializeField] private List<BuffData> stats = new List<BuffData>();

    public override void Excute()
    {
        foreach (var stat in stats)
        {
            Manager.Data.PlayerBuff(stat.type, stat.amount, stat.duration);
        }
    }
}
