using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3 moveDir { get; private set; }
    public Vector3 camDir { get; private set; }
    public Vector3 lastMoveDir;

    private void Update()
    {
        HandleInput();
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
    }
}
