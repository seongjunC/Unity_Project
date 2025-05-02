using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Stat
{
    public int baseStat;
    public List<int> modifiers = new List<int>();

    public int GetValue()
    {
        int totalValue = baseStat;

        foreach (var m in modifiers)
        {
            totalValue += m;
        }

        return totalValue;
    }

    public void AddModifiers(int value)
    {
        modifiers.Add(value);
    }
    public void RemoveModifiers(int value)
    {
        modifiers.Remove(value);
    }
    public void SetBaseStat(int value)
    {
        baseStat = value;
    }
}
