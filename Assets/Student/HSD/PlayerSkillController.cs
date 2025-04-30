using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : SkillController
{
    [SerializeField] private Skill[] skills;

    protected override void Awake()
    {
        base.Awake();

        SetupPlayerSkill();

        ownerSkills = Manager.Data.playerStatus.playerSkills;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Manager.Data.playerStatus.skillUnlock[0])
            UseSKill(ownerSkills[0]);

        if (Input.GetKeyDown(KeyCode.E) && Manager.Data.playerStatus.skillUnlock[1])
            UseSKill(ownerSkills[1]);

        if (Input.GetKeyDown(KeyCode.R) && Manager.Data.playerStatus.skillUnlock[2])
            UseSKill(ownerSkills[2]);

        if (Input.GetKeyDown(KeyCode.V) && Manager.Data.playerStatus.skillUnlock[3])
            UseSKill(ownerSkills[3]);
    }

    private void SetupPlayerSkill()
    {
        Manager.Data.playerStatus.SetupSkills(skills);
    }
}
