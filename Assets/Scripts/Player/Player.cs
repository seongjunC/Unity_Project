using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckPoint;

    public Animator anim;
    public Rigidbody rigid;
    public StateController stateCon;
    public GameObject attackHitbox;
    public PlayerStatusData status;
    public float moveSpeed;
    public float rotateSpeed;

    public Vector3 moveDir;

    // 유니티에서 Ground 레이어 추가하기
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.2f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        stateCon = GetComponent<StateController>();

        status = new PlayerStatusData();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        moveDir = new Vector3(x, 0, z);

        if(status.curHP <= 0)
        {
            stateCon.stateMachine.ChangeState(stateCon.dieState);
        }
    }
    public bool IsGrounded()
    {
        return Physics.Raycast(groundCheckPoint.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    //private void Rotate()
    //{
    //    float input = Input.GetAxis("Horizontal");
    //    //player.transform.Rotate(Vector3.up, player.rotateSpeed * input * Time.deltaTime);
    //}
}