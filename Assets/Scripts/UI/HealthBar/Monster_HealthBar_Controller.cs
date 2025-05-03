using UnityEngine;

public class Monster_HealthBar_Controller : MonoBehaviour
{
    [SerializeField] private Monster_HealthBar monster_HealthBar;

    private void OnEnable()
    {
        monster_HealthBar.monsterStatusCon.status.OnHealthChanged += _ => ActiveHealthBar();
        monster_HealthBar.monsterStatusCon.OnSettigEnded += AddEvent;
        monster_HealthBar.gameObject.SetActive(false);
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
