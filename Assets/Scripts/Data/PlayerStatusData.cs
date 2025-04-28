using EnumType;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusData
{
    public int baseDamage;
    public int baseMaxHP;

    public int damage { get { int totalValue = baseDamage; foreach (int i in damageModifier) { totalValue += i; } return totalValue; } }
    private List<int> damageModifier = new List<int>();
    public int maxHP { get { int totalValue = 0; foreach (int i in hpModifier) { totalValue += i; } return totalValue; } }
    private List<int> hpModifier = new List<int>();

    private int _curHP;
    public int curHP { get => _curHP; set { _curHP = value; OnHPChanged?.Invoke(_curHP); } }
    private int _curExp;
    public int curExp { get => curExp; set { _curExp = value; OnExpChanged?.Invoke(_curExp); } }

    public event Action<int> OnHPChanged;
    public event Action<int> OnExpChanged;

    public PlayerLevelData levelExpData = ScriptableObject.CreateInstance<PlayerLevelData>();

    public void AddModifier(int amount, StatType type)
    {
        switch(type)
        {
            case StatType.Damage:
                damageModifier.Add(amount); break;
            case StatType.MaxHP:
                hpModifier.Add(amount); break;
        }
    }
    public void RemoveModifier(int amount, StatType type)
    {
        switch (type)
        {
            case StatType.Damage:
                damageModifier.Remove(amount); break;
            case StatType.MaxHP:
                hpModifier.Remove(amount); break;
        }
    }
}
