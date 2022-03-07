using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private GameObject skillSelectorUI;
    [SerializeField] private GameObject skillsUI;
    [SerializeField] private GameObject skillInformationUI;

    [SerializeField] private Skill currentSkill;
    [SerializeField] private SkillData currentSkillData;
    [SerializeField] private GameObject player;

    public void SetCurrentSkill(int currentSkillID)
    {
        currentSkill = Database.instance.skillObjects.Find(a => a.SkillID == currentSkillID);
    }
    public void SetCurrentSkillData(int currentSkillDataID)
    {
        currentSkillData = currentSkill.SkillData.Find(a => a.DataID == currentSkillDataID);
    }
    public void UpgradeSkill()
    {
        player.GetComponent<PlayerSkills>().OnButtonPressed(currentSkillData);
    }
}
