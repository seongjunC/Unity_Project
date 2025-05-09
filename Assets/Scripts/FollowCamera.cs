using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraBoom;
    public float mouseSensitivity = 60;
    private float x, y;
    private float xRot, yRot;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void LateUpdate()
    {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        xRot -= y * mouseSensitivity * Time.deltaTime;
        yRot += x * mouseSensitivity * Time.deltaTime;
        xRot = Mathf.Clamp(xRot, -80, 80);

        cameraBoom.rotation = Quaternion.Euler(xRot, yRot, 0);
    }
}
