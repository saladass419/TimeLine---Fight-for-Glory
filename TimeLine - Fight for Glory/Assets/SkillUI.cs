using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private GameObject skillSelectorUI;
    [SerializeField] private GameObject skillsUI;
    [SerializeField] private GameObject skillInformationUI;

    [SerializeField] private Skill currentSkill;
    [SerializeField] private SkillData currentSkillData;
    [SerializeField] private int currentSkillLevelToUpgradeTo;
    [SerializeField] private GameObject player;

    private void Start()
    {
        foreach(var item in FindObjectsOfType<CurrentSkillOnUI>())
        {
            item.onButtonClicked += SetCurrentSkill;
        }
        foreach (var item in FindObjectsOfType<CurrentSkillDataOnUI>())
        {
            item.onButtonClicked += SetCurrentSkillData;
        }
    }
    public void SetCurrentSkill(Skill skill)
    {
        currentSkill = skill;
    }
    public void SetCurrentSkillData(SkillData skillData, int level)
    {
        currentSkillData = skillData;
        currentSkillLevelToUpgradeTo = level;
    }
    public void UpgradeSkill()
    {
        player.GetComponent<PlayerSkills>().OnButtonPressed(currentSkillData, currentSkillLevelToUpgradeTo);
    }
}
