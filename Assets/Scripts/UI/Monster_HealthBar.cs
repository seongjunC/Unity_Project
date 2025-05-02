using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class Monster_HealthBar : BillbordObejct
{
    private MonsterStatusController monsterStatusCon;
    [SerializeField] private Slider slider;
    [SerializeField] private Slider easeSlider;
    [SerializeField] private float easeSpeed;

    Coroutine easeRoutine;

    private void Awake()
    {
        //StartCoroutine(TestRoutine());
        monsterStatusCon = GetComponentInParent<MonsterStatusController>();
    }

    private void OnEnable()
    {
        monsterStatusCon.OnSettigEnded += AddEvent;     
    }

    private void OnDisable()
    {
        monsterStatusCon.status.OnHealthChanged -= UpdateHealthBar;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void AddEvent()
    {
        monsterStatusCon.status.OnHealthChanged += UpdateHealthBar;
    }

    private void UpdateHealthBar(int hp)
    {
        slider.maxValue = monsterStatusCon.status.maxHP;
        easeSlider.maxValue = monsterStatusCon.status.maxHP;

        slider.value = hp;

        if(easeRoutine != null)
        {
            StopCoroutine(easeRoutine);
            easeRoutine = null;
        }

        easeRoutine = StartCoroutine(EaseHealthBarRoutine());
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

    IEnumerator TestRoutine() // 테스트
    {
        yield return new WaitForSeconds(4);

        monsterStatusCon.status.OnHealthChanged += UpdateHealthBar;
        Debug.Log("1");
    }
}
