using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Image cooldown;
    public Skill skill {  get; private set; }

    public int needLevel;
    private bool isCooldown = false;

    private void Awake()
    {
        icon ??= GetComponentInChildren<Image>();
        cooldown ??= GetComponentInChildren<Image>();
    }

    public void SetupSkillSlot(Skill _skill)
    {
        if (_skill == null || _skill.metaData == null)
            return;

        skill = _skill;
        skill.ResetCoolTime();
        icon.sprite = skill.metaData.icon;
        needLevel = skill.metaData.needLevel;
        icon.color = Color.gray;
    }

    public void UnlockSkill()
    {
        cooldown.gameObject.SetActive(true);
        icon.color = Color.white;
    }

    private void Update()
    {
        if (!cooldown.gameObject.activeSelf || skill == null) return;

        cooldown.fillAmount = skill.CoolTimeRatio;

        if (isCooldown && cooldown.fillAmount <= 0f)
        {
            //OnCooldownComplete();
            isCooldown = false;
        }
        else if (cooldown.fillAmount > 0f)
        {
            isCooldown = true;
        }
    }

    private void OnCooldownComplete()
    {

    }
}
