using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : SkillController
{
    [SerializeField] private Skill[] skills;
    private Player player;

    protected override void Awake()
    {
        base.Awake();

        SetupPlayerSkill();
        player = GetComponent<Player>();
        ownerSkills = Manager.Data.playerStatus.playerSkills;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Manager.Data.playerStatus.skillUnlock[0])
        {
            if(UseSKill(ownerSkills[0]))
                player.stateCon.stateMachine.ChangeState(player.stateCon.crossSlashState);
        }

        if (Input.GetKeyDown(KeyCode.E) && Manager.Data.playerStatus.skillUnlock[1])
        {
            if(UseSKill(ownerSkills[1]))
                player.stateCon.stateMachine.ChangeState(player.stateCon.powerSkillState);
        }

        if (Input.GetKeyDown(KeyCode.R) && Manager.Data.playerStatus.skillUnlock[2])
        {
            if(UseSKill(ownerSkills[2]))
                player.stateCon.stateMachine.ChangeState(player.stateCon.bladestormState);
        }

        if (Input.GetKeyDown(KeyCode.V) && Manager.Data.playerStatus.skillUnlock[3])
        {
            if (UseSKill(ownerSkills[3]))
                player.stateCon.stateMachine.ChangeState(player.stateCon.ultimateState);
        }
    }

    private void SetupPlayerSkill()
    {
        Manager.Data.playerStatus.SetupSkills(skills);
    }
}
