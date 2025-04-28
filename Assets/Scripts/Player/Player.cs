using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    

    public Animator anim;
    public float moveSpeed;
    public float rotateSpeed;

    // 유니티에서 Ground 레이어 추가하기
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.2f;

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    // 나중에 주석 없애도 됩니다.
}
