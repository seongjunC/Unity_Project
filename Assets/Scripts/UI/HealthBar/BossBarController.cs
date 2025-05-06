using UnityEditor.PackageManager;
using UnityEngine;

public class BossBarController : MonoBehaviour
{
    [SerializeField] private BossHealthBar bossHealthBar;
    [SerializeField] private BossStunBar bossStunBar;
    private MonsterStatusController statusCon;

    public void Init(MonsterStatusController _statusCon)
    {
        statusCon = _statusCon;
        bossHealthBar.monsterStatusCon = statusCon;

        statusCon.status.OnHealthChanged += bossHealthBar.UpdateHealthBar;
        bossHealthBar.UpdateHealthBar(statusCon.status.maxHP);

        statusCon.OnStunGaugeChanged += bossStunBar.UpdateStunBar;

        statusCon.OnDied += DestroyHealthBar;
    }

    private void DestroyHealthBar()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        bossHealthBar.monsterStatusCon.status.OnHealthChanged -= bossHealthBar.UpdateHealthBar;

        statusCon.OnStunGaugeChanged -= bossStunBar.UpdateStunBar;
    }
}
