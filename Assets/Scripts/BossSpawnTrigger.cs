using UnityEngine;

public class BossSpawnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private Transform spawnPoint;
    private bool isSpawn;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isSpawn)
        {
            isSpawn = true;
            BossSpawn();
        }
    }

    private void BossSpawn()
    {
        Instantiate(boss, spawnPoint.position,Quaternion.identity);
    }
}
