using EnumType;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    public int curHP { get => _curHP; private set { _curHP = value; OnHPChanged?.Invoke(_curHP); } }

    private int _curExp;
    public int curExp { get => curExp; private set { _curExp = value; OnExpChanged?.Invoke(_curExp); } }

    private int _level = 1;
    public int level { get => _level; private set { _level = value; OnLevelUp?.Invoke(_level); } }

    public event Action<int> OnHPChanged;
    public event Action<int> OnExpChanged;
    public event Action<int> OnLevelUp;

    public PlayerLevelData levelExpData = new();

    public Skill[] playerSkills {  get; private set; }
    public bool[] skillUnlock {  get; private set; }

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

    public void IncreaseHealth(int amount)
    {
        curHP += amount;

        if(curHP > maxHP)
            curHP = maxHP;
    }
    public void AddExp(int amount)
    {
        curExp += Mathf.RoundToInt((UnityEngine.Random.Range(amount * 0.8f, amount * 1.2f)));

        CheckLevelUp();
    }

    private void CheckLevelUp() // 임시로 여기 두었음 바꿀 수도 있음.
    {
        if(curExp >= levelExpData.GetLevelExp(level))
        {
            LevelUp();    
        }
    }
    private void LevelUp()
    {
        level = _level++;
    }

    public void SetupSkills(Skill[] skills)
    {
        playerSkills = skills;
        skillUnlock = new bool[playerSkills.Length];
    }
}
