using EnumType;
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
    }
    
    public IEnumerator SetupGameDataWithProgress(System.Action<float> onProgress)
    {
        monsterData = new();
        skillData = new();
        playerStatus = new();
        poolData = new();

        inventory = new GameObject("Inventory").AddComponent<Inventory>();
        inventory.transform.SetParent(transform, false);

        equip = new GameObject("Equipment").AddComponent<Equipment>();
        equip.transform.SetParent(transform, false);

        dataSetter = new GameObject("DataSetter").AddComponent<DataSetter>();
        dataSetter.transform.parent = transform;

        yield return null;
        onProgress?.Invoke(0.2f);

        bool dataSetupDone = false;
        dataSetter.OnDataSetupCompleted = () => dataSetupDone = true;
        dataSetter.Init();

        float currentProgress = 0.2f;
        float targetProgress = 0.6f;

        while (!dataSetupDone)
        {
            yield return null;

            currentProgress = Mathf.MoveTowards(currentProgress, targetProgress, Time.deltaTime * 0.2f);
            onProgress?.Invoke(currentProgress);
        }

        playerStatus.SetupPlayerStat();
        playerStatus.critChance.SetBaseStat(10);
        playerStatus.critDamage.SetBaseStat(150);
        Destroy(dataSetter.gameObject);

        onProgress?.Invoke(0.7f);
    }

    public void PostSceneInit()
    {
        Manager.Game.Init();
        Manager.Audio.Init();
        Manager.Pool.Init();
    }

    public void PlayerBuff(StatType type, int amount, float duration)
    {
        playerStatus.BuffRoutine(type, amount, duration);
    }
}
