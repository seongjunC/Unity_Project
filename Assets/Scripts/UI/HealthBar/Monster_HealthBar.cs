using System.Collections;
using UnityEngine;

public class Monster_HealthBar : HealthBar
{
    public MonsterStatusController monsterStatusCon;  

    Coroutine easeRoutine;
    Coroutine healthBarRoutine;

    private float timer;
    [SerializeField] private float duration = 5;

    private void Awake()
    {
        monsterStatusCon = GetComponentInParent<MonsterStatusController>();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void UpdateHealthBar(int hp)
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);

        slider.maxValue = monsterStatusCon.status.maxHP;
        easeSlider.maxValue = monsterStatusCon.status.maxHP;

        slider.value = hp;

        if(easeRoutine != null)
        {
            StopCoroutine(easeRoutine);
            easeRoutine = null;
        }

        easeRoutine = StartCoroutine(EaseHealthBarRoutine());

        if(healthBarRoutine != null)
        {
            StopCoroutine(healthBarRoutine);
            healthBarRoutine = null;
        }

        healthBarRoutine = StartCoroutine(HealthBarActivateRoutine());
    }

    IEnumerator EaseHealthBarRoutine()
    {
        while(easeSlider.value > slider.value + 0.1f)
        {
            easeSlider.value = Mathf.Lerp(easeSlider.value, slider.value, easeSpeed);
            yield return null;
        }

        easeSlider.value = slider.value;
    }

    IEnumerator HealthBarActivateRoutine()
    {
        timer = duration;

        while(timer > 0)
        {
            timer -= Time.deltaTime;

            yield return null;
        }

        gameObject.SetActive(false);
    }
}
