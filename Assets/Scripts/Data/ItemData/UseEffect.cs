using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UseEffect : ScriptableObject
{
    [SerializeField] private float amount;

    public abstract void Excute();
}
