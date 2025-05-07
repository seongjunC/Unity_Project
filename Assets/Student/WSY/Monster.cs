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
    public bool isUnhittable = false;
    private bool isAttack = false;
    private bool isMoving = false;
    private bool isDead = false;
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRadius;

    private readonly int move = Animator.StringToHash("IsMoving");

    protected virtual void Awake()
    {
        statusCon ??= GetComponent<MonsterStatusController>();
        anim ??= GetComponent<Animator>();
        rigid ??= GetComponent<Rigidbody>();

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

        if (target == null) return;

        if (isMoving)
        {
            Move();
        }
        else if (!isAttack)
        {
            Attack();
        }

        if (target != null && Vector3.Distance(transform.position, target.transform.position) > 1f)
        {
            isMoving = true;
            anim.SetBool(move, true);
        }
        else
        {
            isMoving = false;
            anim.SetBool(move, false);
        }
    }

    protected void Move()
    {
        rigid.velocity = Vector3.zero;

        if (target != null)
        {
            

            Vector3 dirToTarget = (target.transform.position - transform.position).normalized;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirToTarget), rotationSpeed * Time.deltaTime);
            transform.Translate(dirToTarget * statusCon.status.speed * Time.deltaTime, Space.World);
        }
    }

    protected void Attack()
    {
        Vector3 dirToTarget = (target.transform.position - transform.position).normalized;

        if (!(Vector3.Dot(transform.forward, dirToTarget) > Mathf.Cos(fov / 2 * Mathf.Deg2Rad))) return;

        isMoving = false;
        isAttack = true;
        anim.SetTrigger("Attack");
    }

    private void Attacking()
    {
        Collider[] others = Physics.OverlapSphere(attackTransform.position, attackRadius, targetMask);

        foreach (var other in others)
        {
            if (other.CompareTag("Player"))
            {
                IDamagable target = other.GetComponent<IDamagable>();
                target.TakeDamage(statusCon.status.damage);

                StartCoroutine(ResetAttackState(1f));

                break;
            }
        }
    }

    IEnumerator ResetAttackState(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttack = false;
    }

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