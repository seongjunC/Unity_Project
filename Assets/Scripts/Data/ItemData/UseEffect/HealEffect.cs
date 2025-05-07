using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "Data/ItemData/Effect/Heal")]
public class HealEffect : UseEffect
{
    public override void Excute()
    {
        Manager.Data.playerStatus.IncreaseHealth(amount);
    }
}
