using UnityEngine;
using UnityEngine.UI;

public class MouseS : MonoBehaviour
{
    private FollowCamera followCamera;
    [SerializeField] private Slider slider;
    private void Awake()
    {
        followCamera = Camera.main.GetComponentInChildren<FollowCamera>();
        slider.maxValue = 500;
        slider.minValue = 0;
    }

    public void Mouse(float value)
    {
        followCamera.mouseSensitivity = value;
    }
}
