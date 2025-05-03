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

    public void Init()
    {
        camShake = new GameObject("CamShack").AddComponent<CameraShake>();
        camShake.transform.SetParent(transform, false);

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
}
