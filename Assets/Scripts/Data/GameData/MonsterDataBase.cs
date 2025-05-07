using EnumType;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterDataBase
{

#if UNITY_EDITOR
    public List<MonsterData> monsterDatas = new(); // 보기 용
#endif

    private Dictionary<MonsterType, MonsterData> monsterDic = new();

    public void AddMonsterData(MonsterType type, MonsterData data)
    {
        monsterDic[type] = data;
    }

    public MonsterData GetMonsterData(MonsterType type)
    {
        var copyData = monsterDic[type];

        return new MonsterData 
        { 
            name = copyData.name,
            maxHP = copyData.maxHP,
            damage = copyData.damage,
            speed = copyData.speed,
            dropGold = copyData.dropGold,
            dropExp = copyData.dropExp,
            range = copyData.range,
        };
    }
}

[Serializable]
public class MonsterData
{
    public string name;
    [SerializeField] private int _hp;
    public int hp {  get => _hp; set { _hp = value; OnHealthChanged?.Invoke(_hp); } }
    public int maxHP;
    public int damage;
    public float speed;
    public int dropGold;
    public int dropExp;
    public float range;

    public event Action<int> OnHealthChanged;
}