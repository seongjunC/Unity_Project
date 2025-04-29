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
    public Vector3 camDir;

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
        //if (stateCon.stateMachine.currentState == stateCon.dieState)
        //{
        //    return;
        //}

        HandleInput();

        //if(status.curHP <= 0)
        //{
        //    stateCon.stateMachine.ChangeState(stateCon.dieState);
        //}
    }
    public bool IsGrounded()
    {
        return Physics.Raycast(groundCheckPoint.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    private void Rotate()
    {
        if (camDir.sqrMagnitude == 0) return;
        Quaternion targetRot = Quaternion.LookRotation(camDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
    }

    private void HandleInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        moveDir = new Vector3(x, 0, z);

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        camDir = camForward * moveDir.z + camRight * moveDir.x;

        if (camDir.sqrMagnitude > 0)
            Rotate();
    }
}