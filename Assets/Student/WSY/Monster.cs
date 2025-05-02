using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour, IDamagable
{
    private MonsterData monsterData;

    [Header("Monster Fields")]
    [SerializeField] protected string name;
    [SerializeField] protected int hp;
    [SerializeField] protected int damage;
    [SerializeField] protected float speed;
    [SerializeField] protected int gold;
    [SerializeField] protected int exp;
    [SerializeField] protected float detectRadius;
    [SerializeField] protected EnumType.MonsterType monsterType;
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected Animator animator;
    protected bool isDead = false;

    // 이 씬 내에 플레이어 태그를 가진 오브젝트가 있는지 살피고
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    // 몬스터 생성자(와 동일한 역할)
    private void Start()
    {
        // 여기를 특정 타입을 안 쓰고 할 수 있게끔 몬스터 타입을 변수로 두었음.
        monsterData = Manager.Data.monsterData.GetMonsterData(monsterType);
        
        // 데이터 할당해주기
        name = monsterData.name;
        hp = monsterData.health;
        damage = monsterData.damage;
        speed = monsterData.speed;
        gold = monsterData.dropGold;
        exp = monsterData.dropExp;
        detectRadius = monsterData.range;

        // 몬스터 레벨을 플레이어의 레벨에 따라 조정.
        // 몬스터의 최대 레벨은 20으로
        int playerLevel = Manager.Data.playerStatus.level;
        int monsterlevel = Mathf.Clamp(Random.Range(playerLevel - 2, playerLevel - 1), 1, 20);
    }

    void Update()
    {
        Move();
        Attack();
        Die();
    }


    protected void Move()
    {
        // 몬스터끼리 밀어내는 가속도를 계속 0으로 초기화해줘서, 자기들끼리 밀긴 하지만 서로 가속도가 붙지는 않게 해주기.
        rigid.velocity = Vector3.zero;

        if (player != null)
        {
            // 플레이어를 바라보게 회전하고 / 포워드를 해당 방향으로 돌려주는 것. 업데이트마다 할 필요 X
            transform.LookAt(player.transform.position);

            // 플레이어에게 다가가기
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    protected void Attack()
    {
        // 콜라이더로 원형 추적 범위에 들어온 것을 감지하고, (감지 범위 내 물체들은 배열로 저장됨)
        Collider[] others = Physics.OverlapSphere(transform.position, detectRadius, playerLayer);

        foreach (var other in others)
        {
            if (other.CompareTag("Player"))
            {
                // 공격 애니메이션을 할당하고 (트리거 이름 Attack이라고 가정)
                animator.SetTrigger("Attack");

                // 플레이어의 체력을 감소시키기(아래는 직접 수정, 활성화 코드는 IDamagable 사용)
                // Manager.Data.playerStatus.curHP -= damage;
                IDamagable target = other.GetComponent<IDamagable>();
                target.TakeDamage(damage);

                // 플레이어를 찾았으므로 루프 마무리
                break; 
            }
        }
    }

    private void OnDrawGizmos()
    {
        //추적 범위 기즈모도 그려주기 
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }

    protected virtual void Die()
    {
        if (hp == 0 && !isDead)
        {
            // 죽었는지 여부를 true로 바꿔주고 
            isDead = true;

            // 사망 애니메이션 발동하기(트리거 이름 Die로 가정)
            animator.SetTrigger("Die");

            // 플레이어의 골드 증가시키기
            Manager.Game.AddGold((int)(gold * Random.Range(0.8f, 1.1f)));

            // 플레이어의 경험치 증가시키기 (데이터 매니저 통해서)
            Manager.Data.playerStatus.AddExp(exp);

            // 게임 오브젝트를 2초 뒤에 다시 풀로 돌려보내기.
            Manager.Resources.Destroy(gameObject, 2f);

        }
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;

        if(hp <= 0)
        {
            Die();
        }
    }
}
