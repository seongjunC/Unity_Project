using EnumType;
using StructType;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[Serializable]
public class PlayerStatusData
{
    public Stat maxHP = new Stat();
    public Stat damage = new Stat();
    public Stat critChance = new Stat();
    public Stat critDamage = new Stat();

    private int _curHP;
    public int curHP { get => _curHP; private set { _curHP = value; OnHPChanged?.Invoke(_curHP); } }

    private int _curExp;
    public int curExp { get => _curExp; private set { _curExp = value; OnExpChanged?.Invoke(_curExp); } }

    private int _level = 1;
    public int level { get => _level; private set { _level = value; OnLevelUp?.Invoke(_level); } }

    public event Action<int> OnHPChanged;
    public event Action<int> OnExpChanged;
    public event Action<int> OnLevelUp;

    public PlayerLevelDatas levelExpData = new();
    public PlayerStatDatas playerStatData = new();

    public Skill[] playerSkills {  get; private set; }
    public bool[] skillUnlock {  get; private set; }

    public void IncreaseHealth(int amount)
    {
        curHP += amount;

        if(curHP > maxHP.GetValue())
            curHP = maxHP.GetValue();
    }
    public bool DecreaseHealth(float amount)
    {
        curHP -= (int)amount;

        if (curHP <= 0)
        {
            return true;
        }

        return false;
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
        SetupPlayerStat();
    }

    public void SetupSkills(Skill[] skills)
    {
        playerSkills = skills;
        skillUnlock = new bool[playerSkills.Length];
    }

    public void SetupPlayerStat()
    {
        PlayerStatData data = playerStatData.GetStatData(level);

        damage.SetBaseStat(data.damage);

        int _maxHP = playerStatData.GetStatData(level).hp;
        int amount = _maxHP - maxHP.baseStat;
        maxHP.SetBaseStat(_maxHP);

        IncreaseHealth(amount);
    }
}
