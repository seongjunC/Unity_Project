using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterStatusController statusCon;

    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] protected Animator animator;
    [SerializeField] private float rotationSpeed;

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
        rigid.velocity = Vector3.zero;

        if (player != null)
        {
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
                    animator.SetTrigger("Attack");

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, statusCon.status.range);
    }

    private void Die()
    {
        animator.SetTrigger("Die");
    }
}
