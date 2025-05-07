using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Player : MonoBehaviour, ISkillOwner
{
    public Animator anim;
    public Rigidbody rigid;
    public StateController stateCon;
    public PlayerStatusController statusCon;
    public PlayerInput input;
    public Equipment_ItemData curWeapon => Manager.Data.inventory.GetCurrentWeapon();
    public PlayerStatusData status => Manager.Data.playerStatus;

    public bool isSkillActive { get; set; }

    [Header("Move info")]
    public float moveSpeed;
    public float rotateSpeed;
    public float rollForce = 10;


    [Header("Combat info")]
    public Transform attackTransform;
    public Transform[] attackEffectTransform;
    public GameObject defaultAttackEffect;
    public GameObject lastAttackEffect;
    public float attackRaius;
    public float[] attackForce;

    private void Awake()
    {
        anim        = GetComponent<Animator>();
        rigid       = GetComponent<Rigidbody>();
        stateCon    = GetComponent<StateController>();
        input       = GetComponent<PlayerInput>();
        statusCon   = GetComponent<PlayerStatusController>();
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public int GetDamage()
    {
        return status.damage.GetValue();
    }
    public void TakeDamage(int amount)
    {
        hp = Mathf.Max(0, status.curHP - amount);

        var hpProp = typeof(PlayerStatusData).GetProperty("curHP", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        hpProp?.SetValue(status, hp);

        if (status.curHP <= 0)
        {
            stateCon.stateMachine.ChangeState(stateCon.dieState);
        }
    }
}