using UnityEngine;

public class Monster_HealthBar_Controller : MonoBehaviour
{
    [SerializeField] private Monster_HealthBar monster_HealthBar;

    private void Awake()
    {
        monster_HealthBar ??= GetComponentInChildren<Monster_HealthBar>();
    }

    private void OnEnable()
    {
        if (monster_HealthBar == null) return;

        monster_HealthBar.monsterStatusCon.status.OnHealthChanged += _ => ActiveHealthBar();
        monster_HealthBar.monsterStatusCon.OnSettingEnded += AddEvent;
        monster_HealthBar.gameObject.SetActive(false);
    }

    public void Init(MonsterStatusController statusCon)
    {
        monster_HealthBar.monsterStatusCon = statusCon;

        monster_HealthBar.monsterStatusCon.status.OnHealthChanged += monster_HealthBar.UpdateHealthBar;

        monster_HealthBar.UpdateHealthBar(statusCon.status.maxHP);
    }

    private void OnDisable()
    {
        monster_HealthBar.monsterStatusCon.status.OnHealthChanged -= _ => ActiveHealthBar();
        monster_HealthBar.monsterStatusCon.status.OnHealthChanged -= monster_HealthBar.UpdateHealthBar;
    }

    private void AddEvent()
    {
        monster_HealthBar.monsterStatusCon.status.OnHealthChanged += monster_HealthBar.UpdateHealthBar;
    }

    private void ActiveHealthBar()
    {
        monster_HealthBar.gameObject.SetActive(true);
    }
}
