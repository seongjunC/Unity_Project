using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float moveSpped;
    Vector3 moveDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float x, z;
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        moveDir = new Vector3(x,0,z);
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * moveSpped;
    }
}
