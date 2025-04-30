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
        return monsterDic[type];
    }
}

[Serializable]
public class MonsterData
{
    public string name;
    public int health;
    public int damage;
    public float speed;
    public int dropGold;
    public int dropExp;
    public float range;
}