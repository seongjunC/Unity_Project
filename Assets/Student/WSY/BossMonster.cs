using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Monster
{
    void Update()
    {
        Move();
        Attack();
        BossAttack();
        BossDie();
    }

    private void BossAttack()
    {
        // 콜라이더로 원형 추적 범위에 들어온 것을 감지하고, (감지 범위 내 물체들은 배열로 저장됨, 2배 넓은 범위 감지)
        Collider[] others = Physics.OverlapSphere(transform.position, detectRadius * 2, playerLayer);

        foreach (var other in others)
        {
            if (other.CompareTag("Player"))
            {
                // 보스만의 공격 애니메이션 (예: "BossAttack"이라는 트리거)
                animator.SetTrigger("BossAttack");

                // 플레이어의 체력을 감소시키기(아래는 직접 수정, 활성화 코드는 IDamagable 사용)
                // Manager.Data.playerStatus.curHP -= damage;
                IDamagable target = other.GetComponent<IDamagable>();
                target.TakeDamage(damage*2);

                break;
            }
        }
    }

    private void BossDie()
    {
        if (hp == 0 && !isDead)
        {
            // 죽었는지 여부를 true로 바꿔주고 
            isDead = true;

            // 사망 애니메이션 발동하기(트리거 이름 BossDie로 가정)
            animator.SetTrigger("BossDie");

            // 플레이어의 골드 증가시키기
            //Manager.Data.playerStatus.(골드 추가 시) += gold * Random.Range(0.8f, 1.1f);

            // 플레이어의 경험치 증가시키기 (데이터 매니저 통해서)
            Manager.Data.playerStatus.curExp += exp;

            // 게임 오브젝트를 2초 뒤에 다시 풀로 돌려보내기.
            Manager.Resources.Destroy(gameObject, 2f);

        }
    }

}
