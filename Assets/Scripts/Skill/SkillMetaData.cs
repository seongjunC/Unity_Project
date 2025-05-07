

using System;
using UnityEngine;

[Serializable]
public class SkillMetaData
{
    public Sprite icon;
    [TextArea]
    public string skillDescription;
    public int needLevel;
}
