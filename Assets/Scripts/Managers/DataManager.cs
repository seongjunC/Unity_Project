using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : Singleton<DataManager>
{
    private DataSetter dataSetter;
    
    public MonsterDataBase monsterData;
    public PlayerStatusData playerStatus;
    public SkillDataBase skillData;
    public PoolData poolData;

    public Inventory inventory;
    public Equipment equip;

    private void Awake()
    {
        monsterData = new();
        skillData = new();
        playerStatus = new();
        poolData = new();

        dataSetter = new GameObject("DataSetter").AddComponent<DataSetter>();
        dataSetter.transform.parent = transform;          
    }
    private void Start()
    {
        dataSetter.OnDataSetupCompleted = () =>
        {
            SceneManager.LoadSceneAsync("HSDTestScene");

            playerStatus.SetupPlayerStat();

            StartCoroutine(Init());

            playerStatus.critChance.SetBaseStat(10);
            playerStatus.critDamage.SetBaseStat(150);
            Destroy(dataSetter.gameObject);
        };

        dataSetter.Init();
    }

    IEnumerator Init()
    {
        yield return null;

        inventory = new GameObject("Inventory").AddComponent<Inventory>();
        inventory.transform.SetParent(transform, false);

        equip = new GameObject("Equipment").AddComponent<Equipment>();
        equip.transform.SetParent(transform, false);

        yield return null;
        Manager.Audio.Init();
        Manager.Pool.Init();
    }
}
