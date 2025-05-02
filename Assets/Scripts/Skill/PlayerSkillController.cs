using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : SkillController
{
    [SerializeField] private Skill[] skills;
    private StateController player;

    protected override void Awake()
    {
        base.Awake();

        SetupPlayerSkill();
        player = GetComponent<StateController>();
        ownerSkills = Manager.Data.playerStatus.playerSkills;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Manager.Data.playerStatus.skillUnlock[0])
        {
            if(UseSKill(ownerSkills[0]))
                player.stateMachine.ChangeState(player.crossSlashState);
        }

        if (Input.GetKeyDown(KeyCode.E) && Manager.Data.playerStatus.skillUnlock[1])
        {
            if(UseSKill(ownerSkills[1]))
                player.stateMachine.ChangeState(player.powerSkillState);
        }

        if (Input.GetKeyDown(KeyCode.R) && Manager.Data.playerStatus.skillUnlock[2])
        {
            if(UseSKill(ownerSkills[2]))
                player.stateMachine.ChangeState(player.bladestormState);
        }

        if (Input.GetKeyDown(KeyCode.V) && Manager.Data.playerStatus.skillUnlock[3])
        {
            if (UseSKill(ownerSkills[3]))
                Debug.Log("Play Ult");
        }
    }

    private void SetupPlayerSkill()
    {
        Manager.Data.playerStatus.SetupSkills(skills);
    }
}
