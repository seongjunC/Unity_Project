using EnumType;
using System;
using UnityEngine;

public class MonsterStatusController : StatusController
{
    public MonsterData status;

    [Header("Monster Fields")]
    [SerializeField] private int level;
    [SerializeField] private float detectRadius;
    [SerializeField] private MonsterType monsterType;

    [SerializeField] private bool isBoss;
    private float _stunGauge;
    public float stunGauge { get => _stunGauge; set { _stunGauge = value; OnStunGaugeChanged?.Invoke(_stunGauge); } }

    public Action<float> OnStunGaugeChanged;
    public Action OnDied;
    public Action OnHitted;
    public Action OnSettingEnded;

    protected override void Awake()
    {
        base.Awake();
        int playerLevel = Manager.Data.playerStatus.level;
        level = Mathf.Clamp(UnityEngine.Random.Range(playerLevel - 2, playerLevel - 1), 1, 20);
    }

    private void Start()
    {
        SetupLevelStat();
    }

    public override void TakeDamage(float amount, bool isHitter = false)
    {
        if (isDead) return;
        
        DamageCalulator.PlayerAttackCalculator(Manager.Data.playerStatus, this, amount);

        if (isHitter)
            OnHitted?.Invoke();
    }

    public void DcreaseHealth(int amount, bool isCrit = false)
    {
        status.hp -= amount;
        stunGauge -= 0.04f;

        fx.CreatePopUpText(amount, isCrit);

        if (status.hp <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
        OnDied?.Invoke();
        Manager.Game.AddGold(Mathf.RoundToInt(status.dropGold * UnityEngine.Random.Range(0.8f, 1.1f)));

        Manager.Data.playerStatus.AddExp(status.dropExp);

        Manager.Resources.Destroy(gameObject, 2f);
    }

    private void SetupLevelStat()
    {
        status = Manager.Data.monsterData.GetMonsterData(monsterType);
        OnSettingEnded?.Invoke();

        status.damage *= 1 + level / 4;
        status.maxHP *= 1 + level / 4;
        status.hp = status.maxHP;
        stunGauge = 1;
    }
}
