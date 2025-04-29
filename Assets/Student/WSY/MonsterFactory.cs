using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MonsterFactory : MonoBehaviour
{
    [Header("Monster Factory Fields")]
    [SerializeField] private int spawnNum;
    [SerializeField] private float spawnTime;
    [SerializeField] private int initialMonsterNum;
    [SerializeField] private int cumulativeMonsterNum;
    [SerializeField] private int targetcumMonsterNum;
    [SerializeField] private GameObject spawnpoint;

    [Header("Prefabs")]
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private GameObject orcPrefab;
    [SerializeField] private GameObject goblinPrefab;


    [SerializeField] private PoolManager poolManager;
    private float spawnTimer;
    private bool isWaitingToSpawn;


    void Start()
    {
        // 씬마다 몬스터의 종류가 달라지므로 현재 씬 이름 확인, 그에 따른 몬스터의 종류 결정
        
        
        // 게임 시작할 때,
        // 오브젝트 풀 매니저에 Slime/Orc/Goblin 각 몬스터의 프리팹을 오브젝트 풀에 등록해주기.
        poolManager.Get(slimePrefab, Vector3.zero, Quaternion.identity);
        poolManager.Get(orcPrefab, Vector3.zero, Quaternion.identity);
        poolManager.Get(goblinPrefab, Vector3.zero, Quaternion.identity);

        // 초기 몬스터 생성(몇 마리로, 어떤 것으로 할지는 결정 필요 + 아니면 그냥 배치하기?)
        for (int i = 0; i < initialMonsterNum; i++)
        {
            // SpawnMonster("Slime");
        }


    }

    void Update()
    {
        
        
    }


    public Monster Create(string name)
    {
        // 현재까지의 누적 몬스터수가 목표 몬스터수보다 적다면 
        if (cumulativeMonsterNum < targetcumMonsterNum + 1)
        {
            GameObject detectedMonster = GameObject.FindWithTag("Monster");
            
            // 맵 안에 몬스터가 한 마리도 없다면
            if (detectedMonster == null)
            {
                // 스폰 타이머 시작,
                // 타이머가 다 되면 몬스터를 spawnNum만큼 생성.(오브젝트 풀에서.)


                
                // spawnpoint에 spawnTime 후에 spawnNum 마리 생성
                // (오브젝트가 풀 사용 -> 게임오브젝트 몬스터 = Instantiate(사용할 프리팹,스폰포인트, 쿼터니언.아이덴티티)
                // 이 아래 내용을 오브젝트 풀을 사용해서 어떻게 할것인가.
                // 이 아래 애들을 가지고 프리팹을 만들까? 
                // 그리고 그 만든 프리팹을 가지고 인스턴티에트로 만들어주기?
                Monster monster;
                switch (name)
                {
                    // TODO: 여길 데이터 매니저를 활용해서 조금 더 간단히 할 수 있을지?(직접 값을 넣어주는 게 아니라, 데이터 매니저의  
                    case "Slime": monster = new Monster("Slime", 100, 10, 10, exp, detectRadius); break;
                    case "Orc": monster = new Monster("Orc", 231, 15, 8, exp, detectRadius); break;
                    case "Goblin": monster = new Monster("Goblin", 70, 13, 15, exp, detectRadius); break;
                    default: return null;
                }

                return monster;
            }
        }
    }

}
