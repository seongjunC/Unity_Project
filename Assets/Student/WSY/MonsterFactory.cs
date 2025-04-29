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
    // � �������� ����� ���Ŀ� ���� � ������ � ���Ͱ� �����Ǵ����� �����Ǵϱ� �ϳ��� �־ ��.
    [SerializeField] private GameObject monsterPrefab;

    private float spawnTimer;
    private bool isWaitingToSpawn;



    void Start()
    {
        // ������Ʈ Ǯ �Ŵ������� �������� ������Ʈ Ǯ�� ������ֱ�. 
        Manager.Resources.Instantiate(monsterPrefab, Vector3.zero, true);

        // �ʱ� �ʿ� �ִ� ������ ���� (����: 2����)
        cumulativeMonsterNum = 2;
    }

    void Update()
    {
        Create();
    }

    public void Create()
    {
        // ��������� ���� ���ͼ��� ��ǥ ���ͼ����� ���ٸ� 
        if (cumulativeMonsterNum < targetcumMonsterNum + 1)
        {
            // ���� �±׸� ���� ���� ������Ʈ ã��
            GameObject detectedMonster = GameObject.FindWithTag("Monster");

            // �� �ȿ� ���Ͱ� �� ������ ���ٸ�
            if (detectedMonster == null)
            {
                // ���� Ÿ�̸� ����, �� ��ٸ��� ������ ��ٸ���� �ٲٰ�, �ð� 0���� �ʱ�ȭ ���ֱ�.
                if (!isWaitingToSpawn)
                {
                    isWaitingToSpawn = true;
                    spawnTimer = 0f;
                }

                spawnTimer += Time.deltaTime;

                // Ÿ�̸Ӱ� �� �Ǹ� ���͸� spawnNum��ŭ ����.
                if (spawnTimer >= spawnTime)
                {
                    for (int i = 0; i < spawnNum; i++)
                    {
                        SpawnMonster();
                    }

                    // ���� Ÿ�̹� ��� ���ε� �����ϱ�
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
        // ���� ������Ʈ�� ������Ʈ Ǯ����, Instantiate���� �����
        GameObject monster = Instantiate(monsterPrefab, spawnPoint.transform.position, Quaternion.identity);

        // ���� ���� ���� ���� ���������ֱ� 
        cumulativeMonsterNum++;
    }


}
