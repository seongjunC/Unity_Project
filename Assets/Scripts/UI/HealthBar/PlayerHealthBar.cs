using System.Collections;
using UnityEngine;

public class PlayerHealthBar : HealthBar
{
    private PlayerStatusData playerStatusData => Manager.Data.playerStatus;

    Coroutine easeRoutine;

    private void OnEnable()
    {
        playerStatusData.OnHPChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        playerStatusData.OnHPChanged -= UpdateHealthBar;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void UpdateHealthBar(int hp)
    {
        slider.maxValue = playerStatusData.maxHP.GetValue();
        easeSlider.maxValue = playerStatusData.maxHP.GetValue();

        slider.value = hp;

        if (easeRoutine != null)
        {
            StopCoroutine(easeRoutine);
            easeRoutine = null;
        }

        easeRoutine = StartCoroutine(EaseHealthBarRoutine());
    }

    IEnumerator EaseHealthBarRoutine()
    {
        while (easeSlider.value > slider.value + 0.1f)
        {
            easeSlider.value = Mathf.Lerp(easeSlider.value, slider.value, easeSpeed);
            yield return null;
        }

        easeSlider.value = slider.value;
    }
}
