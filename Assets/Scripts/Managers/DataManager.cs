using System.Collections;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private DataSetter dataSetter;
    
    public MonsterDataBase monsterData;
    public PlayerStatusData playerStatus;
    public SkillDataBase skillData;

    public Inventory inventory;

    private void Awake()
    {
        monsterData = new();
        skillData = new();
        playerStatus = new();
        inventory = new GameObject("Inventory").AddComponent<Inventory>();

        dataSetter = new GameObject("DataSetter").AddComponent<DataSetter>();
        dataSetter.transform.parent = transform;          
    }
    private void Start()
    {
        dataSetter.OnDataSetupCompleted = () =>
        {
            playerStatus.SetupPlayerStat();
            Destroy(dataSetter.gameObject);
        };

        dataSetter.Init();
    }
}
