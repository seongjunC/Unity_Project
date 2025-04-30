using EnumType;
using StructType;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DataSetter : MonoBehaviour
{
    private PoolData poolData;
    private MonsterDataBase monsterDataBase;
    private PlayerLevelDatas playerLevelData;
    private SkillDataBase skillDataBase;
    private PlayerStatDatas playerStatData;

    const string poolRange = "A2:B3";
    const string monsterRange = "A2:G4";
    const string levelDataRange = "A2:B18";
    const string playerStatDataRange = "C2:E22";
    const string skillDataRange = "A2:C2";

    #region URL
    // URL : https://docs.google.com/spreadsheets/d/1rqimYysZfUS9PuEodI6qOfoqEmX-2pi6TSmnBiJM810

    string PoolURL = $"https://docs.google.com/spreadsheets/d/1rqimYysZfUS9PuEodI6qOfoqEmX-2pi6TSmnBiJM810/export?format=tsv&range={poolRange}";

    // guid 1001143924
    string MonsterURL = $"https://docs.google.com/spreadsheets/d/1rqimYysZfUS9PuEodI6qOfoqEmX-2pi6TSmnBiJM810/export?format=tsv&gid=1001143924&range={monsterRange}";
    // guid 2123442088
    string LevelURL = $"https://docs.google.com/spreadsheets/d/1rqimYysZfUS9PuEodI6qOfoqEmX-2pi6TSmnBiJM810/export?format=tsv&gid=2123442088&range={levelDataRange}";
    string StatURL = $"https://docs.google.com/spreadsheets/d/1rqimYysZfUS9PuEodI6qOfoqEmX-2pi6TSmnBiJM810/export?format=tsv&gid=2123442088&range={playerStatDataRange}";
    // guid 871025501
    string SkillURL = $"https://docs.google.com/spreadsheets/d/1rqimYysZfUS9PuEodI6qOfoqEmX-2pi6TSmnBiJM810/export?format=tsv&gid=871025501&range={skillDataRange}";

    #endregion

    public void Init()
    {
        poolData = Manager.Pool.poolData;
        monsterDataBase = Manager.Data.monsterData;
        playerLevelData = Manager.Data.playerStatus.levelExpData;
        playerStatData = Manager.Data.playerStatus.playerStatData;
        skillDataBase = Manager.Data.skillData;

        StartCoroutine(DownloadData(PoolURL, DataType.Pool));
        StartCoroutine(DownloadData(MonsterURL, DataType.Monster));
        StartCoroutine(DownloadData(LevelURL, DataType.Level));
        StartCoroutine(DownloadData(SkillURL, DataType.Skill));
        StartCoroutine(DownloadData(StatURL, DataType.PlayerStat));
    }

    IEnumerator DownloadData(string URL, DataType type)
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;

        switch (type)
        {
            case DataType.Pool:
                SetupPoolData(data); break;
            case DataType.Monster:
                SetupMonsterData(data); break;
            case DataType.Level:
                SetupLevelData(data); break;
            case DataType.Skill:
                SetupSkillData(data); break;
            case DataType.PlayerStat:
                SetupStatData(data); break;
            default:
                break;
        }
    }

    private void SetupPoolData(string data)
    {
        string[] row = data.Split("\n");
        int rowSize = row.Length;
        poolData.poolSize = new PoolSize[rowSize];

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');

            poolData.poolSize[i].name = column[0];
            poolData.poolSize[i].size = int.Parse(column[1]);
        }
    }

    private void SetupMonsterData(string data)
    {
        string[] row = data.Split("\n");
        int rowSize = row.Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] columns = row[i].Split("\t");
            MonsterData md = new MonsterData
            {
                name    = columns[0],
                health  = int.Parse(columns[1]),
                damage  = int.Parse(columns[2]),
                speed   = float.Parse(columns[3]),
                dropGold = int.Parse(columns[4]),
                dropExp = int.Parse(columns[5]),
                range   = float.Parse(columns[6]),
            };

            monsterDataBase.AddMonsterData(
                (MonsterType)Enum.Parse(typeof(MonsterType), columns[0]), md
            );
#if UNITY_EDITOR
            monsterDataBase.monsterDatas.Add(md);
#endif
        }
    }

    private void SetupLevelData(string data)
    {
        string[] row = data.Split('\n');
        int rowSize = row.Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] columns = row[i].Split('\t');

            playerLevelData.AddLevelExpData(int.Parse(columns[0]), int.Parse(columns[1]));
        }
    }

    private void SetupSkillData(string data)
    {
        string[] row = data.Split("\n");
        int rowSize = row.Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] columns = row[i].Split('\t');

            string skillName = columns[0];
            SkillData skillData = new SkillData {skillName = skillName, skillPower = int.Parse(columns[1]), coolTime = float.Parse(columns[2]) };

            skillDataBase.AddSkillData(skillName, skillData);
        }
    }

    private void SetupStatData(string data)
    {
        string[] row = data.Split("\n");
        int rowSize = row.Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] columns = row[i].Split('\t');

            int level = int.Parse(columns[0]);
            PlayerStatData statData = new PlayerStatData { damage = int.Parse(columns[1]), hp = int.Parse(columns[2]) };

            playerStatData.AddStatData(level, statData);
        }
    }
}
