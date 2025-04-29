using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Image cooldown;
    
    public int needLevel;
    [SerializeField] private Skill skill;

    private bool isCooldown = false;

    private void Awake()
    {
        icon ??= GetComponent<Image>();
    }

    public void SetupSkillSlot(Skill _skill)
    {
        if (skill == null) return;

        skill = _skill;
        icon.sprite = skill.icon;
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

        cooldown.fillAmount = 1f - skill.CoolTimeRatio;

        if (isCooldown && cooldown.fillAmount <= 0f)
        {
            OnCooldownComplete();
            isCooldown = false;
        }
        else if (cooldown.fillAmount > 0f)
            isCooldown = true;
    }

    private void OnCooldownComplete()
    {

    }
}
