using UnityEngine;
using UnityEngine.UI;

public class BossStunBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void UpdateStunBar(float value)
    {
        slider.value = value;
    }
}
