using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerLevelDatas
{
    private Dictionary<int ,int> levelExpDic = new Dictionary<int ,int>();

    public void AddLevelExpData(int level, int exp)
    {
        levelExpDic.Add(level, exp);
    }

    public int GetLevelExp(int level)
    {
        return levelExpDic[level];
    }
}
