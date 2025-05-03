using UnityEngine;

public class Player : MonoBehaviour, ISkillOwner
{
    public Animator anim;
    public Rigidbody rigid;
    public StateController stateCon;
    public PlayerStatusController statusCon;
    public PlayerInput input;
    public PlayerCutSceneController cutSceneCon;
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
    public float[] attackMoveForce;
    public float[] attackForce;
    public bool invincibility;

    public Transform ultTargetTransform;

    private void Awake()
    {
        anim        = GetComponent<Animator>();
        rigid       = GetComponent<Rigidbody>();
        stateCon    = GetComponent<StateController>();
        input       = GetComponent<PlayerInput>();
        cutSceneCon = GetComponent<PlayerCutSceneController>();
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public int GetDamage()
    {
        return status.damage.GetValue();
    }
}