using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private DataSetter dataSetter;
    public MonsterDataBase monsterData;

    private void Awake()
    {
        monsterData = ScriptableObject.CreateInstance<MonsterDataBase>();

        dataSetter = new GameObject("DataSetter").AddComponent<DataSetter>();
        dataSetter.transform.parent = transform;          
    }
    private void Start()
    {
        dataSetter.Init();

        Destroy(dataSetter);
    }
}
