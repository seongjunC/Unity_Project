using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlotPanel : MonoBehaviour
{
    [SerializeField] private SkillSlot[] skillSlots;

    private void Start()
    {
        SetupSkillSlot();
    }

    private void OnEnable()
    {
        Manager.Data.playerStatus.OnLevelUp += UnlockSkill;
    }
    private void OnDisable()
    {
        Manager.Data.playerStatus.OnLevelUp -= UnlockSkill;
    }

    public void SetupSkillSlot()
    {
        for (int i = 0; i < skillSlots.Length; i++)
        {
            skillSlots[i].SetupSkillSlot(Manager.Data.playerStatus.playerSkills[i]);
        }
    }

    public void UnlockSkill(int level)
    {
        for (int i = 0; i < skillSlots.Length; i++)
        {
            if (Manager.Data.playerStatus.skillUnlock[i] == false && skillSlots[i].needLevel <= level)
            {
                Manager.Data.playerStatus.skillUnlock[i] = true;
                skillSlots[i].UnlockSkill();
            }
        }
    }
}
