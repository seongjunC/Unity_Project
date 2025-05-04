using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [SerializeField] public Rigidbody rigid;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] public Animator anim;
    [SerializeField] private float rotationSpeed;

    GameObject player;
    bool isAttacking = false;

    public MonsterStatusController statusCon;


    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        statusCon = GetComponent<MonsterStatusController>();
        anim = GetComponent<Animator>();

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
        rigid.velocity = Vector3.zero;

        if (player != null)
        {
            // 몬스터랑 플레이어의 직선거리를 보아서, 아직 도달 전이면 (0.1보다 크면?)
            bool isMoving = Vector3.Distance(transform.position, player.transform.position) > 0.1f;
            anim.SetBool("IsMoving", isMoving);

            Vector3 newPos = player.transform.position;
            newPos.y = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newPos), rotationSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, newPos, statusCon.status.speed * Time.deltaTime);
        }
    }

    protected void Attack()
    {
        Collider[] others = Physics.OverlapSphere(transform.position, statusCon.status.range, playerLayer);

        foreach (var other in others)
        {
            if (other.CompareTag("Player"))
            {
                if (!isAttacking)
                {
                    isAttacking = true;
                    anim.SetTrigger("Attack");

                    IDamagable target = other.GetComponent<IDamagable>();
                    target.TakeDamage(statusCon.status.damage);

                    StartCoroutine(ResetAttackState(1f));
                }
                break;
            }
        }
    }
    IEnumerator ResetAttackState(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.cyan;
    //    Gizmos.DrawWireSphere(transform.position, statusCon.status.range);
    //}


    private void Die()
    {
        anim.SetTrigger("Die");
    }
}