using StructType;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillDataBase
{
    private Dictionary<string, SkillData> skillDataDic = new Dictionary<string, SkillData>();

    public void AddSkillData(string name, SkillData skillData)
    {
        skillDataDic.Add(name, skillData);
    }
    public SkillData GetSkillData(string name)
    {
        Debug.Log(skillDataDic[name]);
        return skillDataDic[name];
    }
}
