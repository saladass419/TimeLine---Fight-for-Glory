using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private GameObject skillSelectorUI;
    [SerializeField] private GameObject skillsUI;
    [SerializeField] private GameObject skillInformationUI;

    private SkillData currentSkillData;
    private int currentSkillLevelToUpgradeTo;
    private GameObject player;
    public GameObject Player { get => player; set => player = value; }

    private void Start()
    {
        foreach (var item in FindObjectsOfType<CurrentSkillDataOnUI>())
        {
            item.onButtonClicked += SetCurrentSkillData;
        }
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
