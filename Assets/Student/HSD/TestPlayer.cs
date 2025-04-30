using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestPlayer : MonoBehaviour, ISkillOwner
{
    [SerializeField] private Skill skill;
    private SkillController skillManager;

    Rigidbody rb;
    [SerializeField] private float moveSpped;
    [SerializeField] private float rotSpeed;
    Vector3 moveDir;
    Vector3 camDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        skillManager = GetComponent<SkillController>();
    }

    private void Update()
    {
        float x, z;
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        moveDir = new Vector3(x, 0, z);

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        camDir = camForward * moveDir.z + camRight * moveDir.x;

        if(camDir.sqrMagnitude > 0)
            Rotate();

        if (Input.GetKeyDown(KeyCode.Q))
            skillManager.UseSKill(skill);
    }

    private void Rotate()
    {
        Quaternion targetRot = Quaternion.LookRotation(camDir);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        rb.velocity = camDir * moveSpped;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public int GetDamage()
    {
        return 1;
    }
}
