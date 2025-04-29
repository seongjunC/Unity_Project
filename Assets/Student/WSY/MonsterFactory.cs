using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterFactory : MonoBehaviour
{
    [Header("Monster Factory Fields")]
    [SerializeField] private int spawnNum;
    [SerializeField] private float spawnTime;
    [SerializeField] private int initialMonsterNum;
    [SerializeField] private int cumulativeMonsterNum;
    [SerializeField] private int targetcumMonsterNum;
    [SerializeField] private GameObject spawnPoint;

    [Header("Prefabs")]
    // 어떤 프리팹을 끌어다 놓냐에 따라서 어떤 씬에서 어떤 몬스터가 생성되는지가 결정되니까 하나만 있어도 됨.
    [SerializeField] private GameObject monsterPrefab;

    private float spawnTimer;
    private bool isWaitingToSpawn;



    void Start()
    {
        // 오브젝트 풀 매니저에서 프리팹을 오브젝트 풀에 등록해주기. 
        Manager.Resources.Instantiate(monsterPrefab, Vector3.zero, true);

        // 초기 맵에 있는 몬스터의 갯수 (예시: 2마리)
        cumulativeMonsterNum = 2;
    }

    void Update()
    {
        Create();
    }


    public void Create()
    {
        // 현재까지의 누적 몬스터수가 목표 몬스터수보다 적다면 
        if (cumulativeMonsterNum < targetcumMonsterNum + 1)
        {
            // 몬스터 태그를 가진 게임 오브젝트 찾기
            GameObject detectedMonster = GameObject.FindWithTag("Monster");

            // 맵 안에 몬스터가 한 마리도 없다면
            if (detectedMonster == null)
            {
                // 스폰 타이머 시작, 안 기다리고 있으면 기다리기로 바꾸고, 시간 0으로 초기화 해주기.
                if (!isWaitingToSpawn)
                {
                    isWaitingToSpawn = true;
                    spawnTimer = 0f;
                }

                spawnTimer += Time.deltaTime;

                // 타이머가 다 되면 몬스터를 spawnNum만큼 생성.
                if (spawnTimer >= spawnTime)
                {
                    for (int i = 0; i < spawnNum; i++)
                    {
                        SpawnMonster();
                    }

                    // 스폰 타이밍 대기 여부도 해제하기
                    isWaitingToSpawn = false;

                }
            }

        }
        else
        {
            isWaitingToSpawn = false;
            spawnTimer = 0f;
        }
    }

    private void SpawnMonster()
    {
        // 몬스터 오브젝트를 오브젝트 풀에서, Instantiate으로 만들기
        GameObject monster = Instantiate(monsterPrefab, spawnPoint.transform.position, Quaternion.identity);

        // 누적 몬스터 생성 갯수 증가시켜주기 
        cumulativeMonsterNum++;
    }


}
