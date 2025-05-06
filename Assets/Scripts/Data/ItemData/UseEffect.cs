using UnityEngine;

public abstract class UseEffect : ScriptableObject
{
    [SerializeField] protected int amount;

    public abstract void Excute();
}
