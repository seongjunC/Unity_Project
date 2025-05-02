using EnumType;
using StructType;
using System;
using System.Collections;
using System.Threading;
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
    const string skillDataRange = "A2:C4";

    public Action OnDataSetupCompleted;

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
        poolData = Manager.Data.poolData;
        monsterDataBase = Manager.Data.monsterData;
        playerLevelData = Manager.Data.playerStatus.levelExpData;
        playerStatData = Manager.Data.playerStatus.playerStatData;
        skillDataBase = Manager.Data.skillData;

        StartCoroutine(DownloadData());
    }

    IEnumerator DownloadData()
    {
        UnityWebRequest www;
        string data;

        // 몬스터 데이터

        www = UnityWebRequest.Get(MonsterURL);

        yield return www.SendWebRequest();
        yield return null;

        data = www.downloadHandler.text;

        SetupMonsterData(data);
        Debug.Log("Monster Data Load");

        // 풀 데이터
        www = UnityWebRequest.Get(PoolURL);
        yield return www.SendWebRequest();
        yield return null;
        data = www.downloadHandler.text;
        SetupPoolData(data);
        Debug.Log("Pool Data Load");

        // 레벨 데이터

        www = UnityWebRequest.Get(LevelURL);

        yield return www.SendWebRequest();
        yield return null;

        data = www.downloadHandler.text;

        SetupLevelData(data);
        Debug.Log("Level Data Load");

        // 스킬 데이터

        www = UnityWebRequest.Get(SkillURL);

        yield return www.SendWebRequest();
        yield return null;

        data = www.downloadHandler.text;

        SetupSkillData(data);
        Debug.Log("Skill Data Load");

        // 스텟 데이터

        www = UnityWebRequest.Get(StatURL);

        yield return www.SendWebRequest();
        yield return null;

        data = www.downloadHandler.text;

        SetupStatData(data);
        Debug.Log("Stat Data Load");

        OnDataSetupCompleted?.Invoke();
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
                maxHP  = int.Parse(columns[1]),
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
            SkillData skillData = new SkillData {skillName = skillName, skillPower = float.Parse(columns[1]), coolTime = float.Parse(columns[2]) };

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
