using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerStatusController : StatusController
{
    public PlayerStatusData status;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        status = Manager.Data.playerStatus;
    }

    public override void TakeDamage(float amount)
    {
        if (status.DecreaseHealth(amount))
            Die();  
    }

    private void Die()
    {
        // stateCon.stateMachine.ChangeState()
    }
}
