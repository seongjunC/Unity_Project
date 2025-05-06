using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private int _curGold;
    public int gold { get { return _curGold; }  private set { _curGold = value; OnGoldChanged?.Invoke(_curGold); } }

    public event Action<int> OnGoldChanged;
    private CameraShake camShake;

    public GameObject canvas;

    public void Init()
    {
        camShake = new GameObject("CamShack").AddComponent<CameraShake>();
        camShake.transform.SetParent(transform, false);
        canvas = GameObject.FindWithTag("UI");
        camShake.Init();
    }

    public void AddGold(int amount) => gold += amount;

    public void RemoveGold(int amount)
    {
        if (gold - amount < 0)
            return;

        gold -= amount;
    }
    public void SetGold(int amount) => gold = amount;   

    public void SlowMotion(float scale, float duration) => StartCoroutine(SlowMotionRoutine(scale, duration));
    public void Shake(float amplitude, float duration, float frequency = 3) => camShake.Shake(amplitude, duration, frequency);
    IEnumerator SlowMotionRoutine(float scale, float duration)
    {
        Time.timeScale = scale;
        yield return new WaitForSeconds(duration);
        Time.timeScale = 1;
    }

    public void CreateBossBarUI(MonsterStatusController statusCon)
    {
        GameObject bossBar = Instantiate(Resources.Load<GameObject>("BossHealthBar"), canvas.transform);

        RectTransform rectTransform = bossBar.GetComponent<RectTransform>();

        rectTransform.anchorMin = new Vector2(0.5f, 0.86829f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.86829f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);

        rectTransform.anchoredPosition = new Vector2(0f, 3f);
        rectTransform.localScale = Vector3.one;
        rectTransform.localRotation = Quaternion.identity;

        BossHealthBar bossHealthBar = bossBar.GetComponentInChildren<BossHealthBar>();
        bossHealthBar.monsterStatusCon = statusCon;

        var controller = bossBar.GetComponent<BossBarController>();
        controller.Init(statusCon);
    }
}
