using System.Collections;
using UnityEngine;

public class BossHealthBar : HealthBar
{
    public MonsterStatusController monsterStatusCon;

    Coroutine easeRoutine;

    protected override void Update()
    {

    }

    public void UpdateHealthBar(int hp)
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        slider.maxValue = monsterStatusCon.status.maxHP;
        easeSlider.maxValue = monsterStatusCon.status.maxHP;

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
