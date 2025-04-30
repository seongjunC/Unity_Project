using StructType;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class PlayerStatDatas
{
    private Dictionary<int, PlayerStatData> playerStats = new Dictionary<int, PlayerStatData>();

    public void AddStatData(int level, PlayerStatData statData)
    {
        playerStats.Add(level, statData);
    }

    public PlayerStatData GetStatData(int level)
    {
        return playerStats[level];
    }
}
