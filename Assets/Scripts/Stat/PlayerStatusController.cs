using System;
using UnityEngine;

public class PlayerStatusController : StatusController
{
    public PlayerStatusData status;
    public bool invincibility;
    [SerializeField] private AudioClip[] hitSound;

    public event Action OnDied;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        status = Manager.Data.playerStatus;
    }

    public override void TakeDamage(float amount, bool isHitter = false)
    {
        if (invincibility)
        {
            Manager.Game.SlowMotion(.2f, .2f);
            return;
        }

        Manager.Audio.PlayEffectAtPoint(hitSound[UnityEngine.Random.Range(0,hitSound.Length)], transform.position, UnityEngine.Random.Range(.8f,1));

        if (status.DecreaseHealth(amount))
            Die();

        fx.CreatePopUpText(Mathf.RoundToInt(amount));
    }

    private void Die()
    {
        OnDied?.Invoke();
    }
}
