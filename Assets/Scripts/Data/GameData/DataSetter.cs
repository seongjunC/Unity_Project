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
    private PlayerLevelData playerLevelData;

    const string poolRange = "A2:B3";
    const string monsterRange = "A2:D4";
    const string levelDataRange = "A2:B18";

    #region URL
    // URL : https://docs.google.com/spreadsheets/d/1rqimYysZfUS9PuEodI6qOfoqEmX-2pi6TSmnBiJM810

    string PoolURL = $"https://docs.google.com/spreadsheets/d/1rqimYysZfUS9PuEodI6qOfoqEmX-2pi6TSmnBiJM810/export?format=tsv&range={poolRange}";

    // guid 1001143924
    string MonsterURL = $"https://docs.google.com/spreadsheets/d/1rqimYysZfUS9PuEodI6qOfoqEmX-2pi6TSmnBiJM810/export?format=tsv&gid=1001143924&range={monsterRange}";

    string levelURL = $"https://docs.google.com/spreadsheets/d/1rqimYysZfUS9PuEodI6qOfoqEmX-2pi6TSmnBiJM810/export?format=tsv&gid=2123442088&range={levelDataRange}";

    #endregion

    public void Init()
    {
        poolData = Manager.Pool.poolData;
        monsterDataBase = Manager.Data.monsterData;

        StartCoroutine(DownloadData(PoolURL, DataType.Pool));
        StartCoroutine(DownloadData(MonsterURL, DataType.Monster));
        StartCoroutine(DownloadData(levelURL, DataType.Level));
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
                name = columns[0],
                health = int.Parse(columns[1]),
                damage = int.Parse(columns[2]),
                speed = float.Parse(columns[3])
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
}
