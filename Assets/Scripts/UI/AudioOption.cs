using UnityEngine;
using UnityEngine.UI;

public class AudioOption : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private string parametr;
    [SerializeField] private float multiplier;

    public void SliderValue(float _value) => Manager.Audio.SliderValue(parametr, _value, multiplier);

    public void LoadSlider(float _value)
    {
        if (_value >= 0.001f)
            slider.value = _value;
    }
}
