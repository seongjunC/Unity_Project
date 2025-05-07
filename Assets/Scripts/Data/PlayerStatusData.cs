using EnumType;
using StructType;
using System;
using System.Collections;
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

    [SerializeField] private int _curExp;
    public int curExp { get => _curExp; private set { _curExp = value; OnExpChanged?.Invoke(_curExp); } }

    private int _level = 1;
    public int level { get => _level; private set { _level = value; OnLevelUp?.Invoke(_level); } }

    public event Action<int> OnHPChanged;
    public event Action<int> OnExpChanged;
    public event Action<int> OnLevelUp;

    public PlayerLevelDatas levelExpData = new();
    public PlayerStatDatas playerStatData = new();

    public Skill[] playerSkills {  get; private set; } = new Skill[4];
    public bool[] skillUnlock {  get; private set; } = new bool[4];

    public void IncreaseHealth(int amount)
    {
        curHP += amount;

        if(curHP > maxHP.GetValue())
            curHP = maxHP.GetValue();
    }

    public bool DecreaseHealth(float amount)
    {
        curHP -= Mathf.RoundToInt(amount);

        if (curHP <= 0)
        {
            return true;
        }

        return false;
    }

    public void AddExp(int amount)
    {
        curExp += Mathf.RoundToInt((UnityEngine.Random.Range(amount * 0.8f, amount * 1.2f)));
        Debug.Log("경헙치 획득");
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        if(curExp >= GetLevelExp())
        {
            LevelUp();    
        }
    }

    public int GetLevelExp()
    {
        return levelExpData.GetLevelExp(level);
    }
    private void LevelUp()
    {
        Debug.Log("레벨 업");
        curExp -= GetLevelExp();
        level++;
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

        curExp = curExp;

        IncreaseHealth(amount);
    }

    public IEnumerator BuffRoutine(StatType type, int amount, float duration)
    {
        GetStat(type).AddModifier(amount);
        yield return new WaitForSeconds(duration);
        GetStat(type).RemoveModifier(amount);
    }

    private Stat GetStat(StatType type)
    {
        return (type) switch
        {
            StatType.Damage => damage,
            StatType.MaxHP => maxHP,
            StatType.CritDamage => critDamage,
            StatType.CritChance => critChance,
            _ => throw new NotSupportedException()
        };
    }
}
