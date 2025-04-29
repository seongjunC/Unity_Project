using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraBoom;
    [SerializeField] private float mouseSensitivity = 60;
    private float x, y;
    private float xRot, yRot;

    private void LateUpdate()
    {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        xRot -= y * mouseSensitivity;
        yRot += x * mouseSensitivity;
        xRot = Mathf.Clamp(xRot, -80, 80);

        cameraBoom.rotation = Quaternion.Euler(xRot, yRot, 0);
    }
}
