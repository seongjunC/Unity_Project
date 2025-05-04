using System;
using UnityEngine;

public class PlayerStatusController : StatusController
{
    public PlayerStatusData status;
    public bool invincibility;

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
        if (invincibility) return;

        if (status.DecreaseHealth(amount))
            Die();

        fx.CreatePopUpText(Mathf.RoundToInt(amount));
    }

    private void Die()
    {
        // stateCon.stateMachine.ChangeState()
    }
}
