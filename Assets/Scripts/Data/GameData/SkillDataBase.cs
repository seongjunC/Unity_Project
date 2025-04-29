using StructType;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataBase : ScriptableObject
{
    private Dictionary<string, SkillData> skillDataDic = new Dictionary<string, SkillData>();

    public void AddSkillData(string name, SkillData skillData)
    {
        skillDataDic.Add(name, skillData);
    }
    public SkillData GetSkillData(string name)
    {
        return skillDataDic[name];
    }
}
