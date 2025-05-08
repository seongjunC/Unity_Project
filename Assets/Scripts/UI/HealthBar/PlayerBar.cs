using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : HealthBar
{
    [SerializeField] private Slider expSlider;
    [SerializeField] private TMP_Text level;
    private PlayerStatusData playerStatus;

    Coroutine easeRoutine;

    private void Awake()
    {
        playerStatus = Manager.Data.playerStatus;
    }

    private void OnEnable()
    {
        playerStatus.OnHPChanged += UpdateHealthBar;
        playerStatus.OnExpChanged += UpdateExpBar;
        playerStatus.OnLevelUp += UpdateLevel;
        UpdateExpBar(playerStatus.curExp);
    }

    private void OnDisable()
    {
        playerStatus.OnHPChanged -= UpdateHealthBar;
        playerStatus.OnExpChanged -= UpdateExpBar;
        playerStatus.OnLevelUp -= UpdateLevel;
    }

    protected override void Update()
    {
        
    }

    private void UpdateLevel(int _level)
    {
        level.text = $"Level : {_level}";
    }

    private void UpdateHealthBar(int hp)
    {
        slider.maxValue = playerStatus.maxHP.GetValue();
        easeSlider.maxValue = playerStatus.maxHP.GetValue();

        slider.value = hp;

        if (easeRoutine != null)
        {
            StopCoroutine(easeRoutine);
            easeRoutine = null;
        }

        easeRoutine = StartCoroutine(EaseHealthBarRoutine());
    }

    private void UpdateExpBar(int exp)
    {
        expSlider.maxValue = playerStatus.GetLevelExp();
        expSlider.value = exp;
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
