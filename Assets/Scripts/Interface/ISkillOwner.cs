using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillOwner
{
    public bool isSkillActive {  get; set; }
    public Transform GetTransform();
    public int GetDamage();
}
