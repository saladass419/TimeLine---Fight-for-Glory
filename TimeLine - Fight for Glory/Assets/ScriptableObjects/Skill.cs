using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

[CreateAssetMenu(menuName = "Skill")]
public class Skill : ScriptableObject
{
    [SerializeField] private SkillType skillType;

    [SerializeField] private int upgradeCostNumber = 2000;
    [SerializeField] private int upgradeAttributeValueNumber = 20;

    [SerializeField] private SkillData[] skillData = new SkillData[3];
    public SkillData[] SkillData { get => skillData; set => skillData = value; }
    private void OnValidate()
    {
        SkillData[0].SkillLevel = SkillLevel.Basic;
        SkillData[1].SkillLevel = SkillLevel.Advanced_a;
        SkillData[2].SkillLevel = SkillLevel.Advanced_b;

        SkillData[0].Level = Mathf.Clamp(SkillData[0].Level, 1, 5);
        SkillData[1].Level = Mathf.Clamp(SkillData[1].Level, 1, 2);
        SkillData[2].Level = Mathf.Clamp(SkillData[2].Level, 1, 2);

        UpgradeAttributes();
        UpdateUpgradeCost();
    }
    private void SubscribeToPlayer()
    {
        FindObjectOfType<PlayerSkills>().upgradeSkillLevel += UpgradeSkill;
    }
    public void UpdateUpgradeCost()
    {
        SkillData[0].UpgradeCost = (upgradeCostNumber/10) * SkillData[0].Level + (SkillData[0].Level - 1) * (upgradeCostNumber/4); 
        SkillData[1].UpgradeCost = upgradeCostNumber * SkillData[1].Level;
        SkillData[2].UpgradeCost = upgradeCostNumber * SkillData[2].Level;
    }
    public void UpgradeAttributes()
    {
        foreach (SkillData skill in skillData)
        {
            foreach (Attribute attribute in skill.Attributes)
            {
                attribute.Value = upgradeAttributeValueNumber * (skill.UpgradeCost / 100);
            }
        }
    }
    public void UpgradeSkill(Skill skill, SkillLevel skillLevel)
    {
        skill.SkillData[((int)skillLevel)].Level++;
        UpdateUpgradeCost();
        UpgradeAttributes();

    }
}
[System.Serializable]
public class SkillData
{
    [SerializeField] private string skillName;
    [SerializeField] private string description;
    [SerializeField] private SkillLevel skillLevel;
    [SerializeField] private int level;
    [SerializeField] private float upgradeCost;
    [SerializeField] private Sprite sprite;
    [SerializeField] private List<Attribute> attributes;
    public List<Attribute> Attributes { get => attributes; set => attributes = value; }
    public int Level { get => level; set => level = value; }
    public float UpgradeCost { get => upgradeCost; set => upgradeCost = value; }
    public SkillLevel SkillLevel { get => skillLevel; set => skillLevel = value; }
}
public enum SkillType
{
    Healer, Tank, Fight, Leprechaun, TimeTraveler, Collector, TrapSpecialist
}
public enum SkillLevel
{
    Basic, Advanced_a, Advanced_b
}
