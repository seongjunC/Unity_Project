using EnumType;
using System;
using System.Collections;
using UnityEngine;

public class MonsterStatusController : StatusController
{
    public MonsterData status;

    [Header("Monster Fields")]
    [SerializeField] private int level;
    [SerializeField] private float detectRadius;
    [SerializeField] private MonsterType monsterType;

    public Action OnDied;
    public event Action OnSettigEnded;
    private bool isDead = false;

    protected override void Awake()
    {
        base.Awake();
        int playerLevel = Manager.Data.playerStatus.level;
        level = Mathf.Clamp(UnityEngine.Random.Range(playerLevel - 2, playerLevel - 1), 1, 20);
    }

    // 몬스터 생성자(와 동일한 역할)
    private void Start()
    {
        //// 여기를 특정 타입을 안 쓰고 할 수 있게끔 몬스터 타입을 변수로 두었음.
        //status = Manager.Data.monsterData.GetMonsterData(monsterType);

        //// 몬스터 레벨을 플레이어의 레벨에 따라 조정.
        //// 몬스터의 최대 레벨은 20으로
        
        //StartCoroutine(DelayInit());
        SetupLevelStat();
    }

    public override void TakeDamage(float amount)
    {
        if (isDead) return;
        
        DamageCalulator.PlayerAttackCalculator(Manager.Data.playerStatus, this, amount);
    }

    public void DcreaseHealth(int amount, bool isCrit = false)
    {
        status.hp -= amount;

        fx.CreatePopUpText(amount, isCrit);

        if (status.hp <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
        OnDied?.Invoke();

        // 플레이어의 골드 증가시키기
        Manager.Game.AddGold(Mathf.RoundToInt(status.dropGold * UnityEngine.Random.Range(0.8f, 1.1f)));

        // 플레이어의 경험치 증가시키기 (데이터 매니저 통해서)
        Manager.Data.playerStatus.AddExp(status.dropExp);

        // 게임 오브젝트를 2초 뒤에 다시 풀로 돌려보내기.
        Manager.Resources.Destroy(gameObject, 2f);
    }

    private void SetupLevelStat()
    {
        status = Manager.Data.monsterData.GetMonsterData(monsterType);
        OnSettigEnded?.Invoke();

        status.damage *= 1 + level / 4;
        status.maxHP *= 1 + level / 4;
        status.hp = status.maxHP;
    }

    IEnumerator DelayInit() // 테스트 용
    {
        yield return new WaitForSeconds(5);
        int playerLevel = Manager.Data.playerStatus.level;
        status = Manager.Data.monsterData.GetMonsterData(monsterType);
        level = Mathf.Clamp(UnityEngine.Random.Range(playerLevel - 2, playerLevel - 1), 1, 20);
        SetupLevelStat();
    }
}
