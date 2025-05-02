using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterStatusController statusCon;

    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected Animator animator;
    [SerializeField] private float rotationSpeed;
    // 이 씬 내에 플레이어 태그를 가진 오브젝트가 있는지 살피고
    GameObject player;
    bool isAttacking = false;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        statusCon = GetComponent<MonsterStatusController>();
    }

    private void OnEnable()
    {
        statusCon.OnDied += Die;
    }

    private void OnDisable()
    {
        statusCon.OnDied -= Die;
    }

    void Update()
    {
        if (!isAttacking)
        {
            Move();
        }
        Attack();

    }


    protected void Move()
    {
        // 몬스터끼리 밀어내는 가속도를 계속 0으로 초기화해줘서, 자기들끼리 밀긴 하지만 서로 가속도가 붙지는 않게 해주기.
        rigid.velocity = Vector3.zero;

        if (player != null)
        {
            // 플레이어를 바라보게 회전하고 / 포워드를 해당 방향으로 돌려주는 것. 업데이트마다 할 필요 X
            Vector3 newPos = player.transform.position;
            newPos.y = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newPos), rotationSpeed * Time.deltaTime);

            // 플레이어에게 다가가기
            transform.position = Vector3.MoveTowards(transform.position, newPos, statusCon.status.speed * Time.deltaTime);
        }
    }

    protected void Attack()
    {
        // 콜라이더로 원형 추적 범위에 들어온 것을 감지하고, (감지 범위 내 물체들은 배열로 저장됨)
        Collider[] others = Physics.OverlapSphere(transform.position, statusCon.status.range, playerLayer);

        foreach (var other in others)
        {
            if (other.CompareTag("Player"))
            {
                if (!isAttacking)
                {
                    // 공격 애니메이션을 할당하고 (트리거 이름 Attack이라고 가정)
                    animator.SetTrigger("Attack");

                    // 플레이어의 체력을 감소시키기(아래는 직접 수정, 활성화 코드는 IDamagable 사용)
                    // Manager.Data.playerStatus.curHP -= damage;
                    IDamagable target = other.GetComponent<IDamagable>();
                    target.TakeDamage(statusCon.status.damage);

                    // 일정 시간 지난 후에 공격 상태 해제해주기 (1초?)
                    // 애니메이션 끝난 시점에서 false로 바꿔주기로 추후 변경가능성?
                    StartCoroutine(ResetAttackState(1f));
                }
               
                // 플레이어를 찾았으므로 루프 마무리
                break; 
            }
        }
    }

    IEnumerator ResetAttackState(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false; // 공격 종료
    }

    private void OnDrawGizmos()
    {
        //추적 범위 기즈모도 그려주기 
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, statusCon.status.range);
    }

    private void Die()
    {
        animator.SetTrigger("Die");
    }
}
