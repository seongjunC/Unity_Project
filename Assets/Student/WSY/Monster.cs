using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    [Header("Monster Fields")]
    [SerializeField] private string name;
    [SerializeField] private int hp;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float gold;
    [SerializeField] private float exp;
    [SerializeField] private float detectRadius;

    [SerializeField] private Rigidbody rigid;
    [SerializeField] private LayerMask playerLayer;

    void Update()
    {
        Move();
        Attack();
    }


    private void Move()
    {
        // 이 씬 내에 플레이어 태그를 가진 오브젝트가 있는지 살피고
        GameObject player = GameObject.FindWithTag("Player");

        // 씬 안에 플레이어가 있다면
        if (player != null)
        {
            // 플레이어를 바라보게 회전하고
            transform.LookAt(player.transform.position * Time.deltaTime);

            // 플레이어에게 다가가기
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
        }
    }

    private void Attack()
    {
        // 콜라이더로 원형 추적 범위에 들어온 것을 감지하고, (감지 범위 내 물체들은 배열로 저장됨)
        Collider[] others = Physics.OverlapSphere(transform.position, detectRadius, playerLayer);

        foreach (var other in others)
        {
            if (other.CompareTag("Player"))
            {
                // 공격 애니메이션을 할당하고 
                // TODO: 공격 애니메이션 할당하기 
                // 플레이어의 체력을 감소시키기 (데이터 매니저 통해서)
                // TODO: 플레이어의 체력 감소 시키기 
                // 플레이어를 찾았으므로 루프 마무리
                break; 
            }
        }
    

        //추적 범위 기즈모도 그려주기 
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
    
    private void Die()
    {
        if(hp == 0)
        {
            // 사망 애니메이션 발동하기
            // TODO: 사망 애니메이션 할당하기 
            // 플레이어의 골드 증가시키기 (데이터 매니저 통해서)
            // TODO: 플레이어의 골드 증가시키기 (데이터 매니저 통해서)
            // 활용할 것: gold x Random.Range(0.8f, 1.1f)
            // 플레이어의 경험치 증가시키기 (데이터 매니저 통해서)
            // TODO: 플레이어의 경험치 증가시키기 (데이터 매니저 통해서)
        }
    }


}
