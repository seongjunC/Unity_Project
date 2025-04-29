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
        monsterData = ScriptableObject.CreateInstance<MonsterDataBase>();
        skillData = ScriptableObject.CreateInstance<SkillDataBase>();
        playerStatus = new PlayerStatusData();
        inventory = new GameObject("Inventory").AddComponent<Inventory>();

        dataSetter = new GameObject("DataSetter").AddComponent<DataSetter>();
        dataSetter.transform.parent = transform;          
    }
    private void Start()
    {
        dataSetter.Init();
        Destroy(dataSetter, 5);
    }
}
