using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Rigidbody rigid;
    public GameObject target;
    public Animator anim;
    public MonsterStatusController statusCon;
    public float fov = 60;

    public LayerMask targetMask;

    public float rotationSpeed;
    public bool isAttacking = false;

    protected virtual void Awake()
    {
        statusCon   ??= GetComponent<MonsterStatusController>();
        anim        ??= GetComponent<Animator>();
        rigid       ??= GetComponent<Rigidbody>();

        targetMask = (1 << LayerMask.NameToLayer("Player"));
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
        FindTarget();

        if (!isAttacking)
        {
            //Move();
        }
        //Attack();

    }

    protected void Move()
    {
        rigid.velocity = Vector3.zero;

        if (target != null)
        {
            // 몬스터랑 플레이어의 직선거리를 보아서, 아직 도달 전이면 (0.1보다 크면?)
            bool isMoving = Vector3.Distance(transform.position, target.transform.position) > 0.1f;
            anim.SetBool("IsMoving", isMoving);

            Vector3 newPos = target.transform.position;
            newPos.y = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newPos), rotationSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, newPos, statusCon.status.speed * Time.deltaTime);
        }
    }

    protected void Attack()
    {
        Collider[] others = Physics.OverlapSphere(transform.position, statusCon.status.range, targetMask);

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

    public void FindTarget()
    {
        if (target != null) return;

        Collider[] targets = Physics.OverlapSphere(transform.position, statusCon.status.range, targetMask); 

        if (targets.Length > 0)
        {
            target = targets[0].gameObject;
        }
    }

    private void Die()
    {
        anim.SetTrigger("Die");
    }
}