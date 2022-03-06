using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

[CreateAssetMenu(menuName = "Skill")]
public class Skill : ScriptableObject
{
    [SerializeField] private SkillType skillType;
    [SerializeField] private SkillLevel skillLevel;
    [SerializeField] private SkillData skillData = new SkillData();

    [SerializeField] private int upgradeCostNumber = 2000;
    [SerializeField] private int upgradeAttributeValueNumber = 20;

    public SkillData SkillData { get => skillData; set => skillData = value; }
    public SkillLevel SkillLevel { get => skillLevel; set => skillLevel = value; }
    public SkillType SkillType { get => skillType; set => skillType = value; }

    private void OnValidate()
    {
        switch (SkillLevel)
        {
            case SkillLevel.Basic:
                SkillData.Level = Mathf.Clamp(SkillData.Level, 0, skillData.MaxLevel);
                break;
            case SkillLevel.Advanced_a:
                SkillData.Level = Mathf.Clamp(SkillData.Level, 0, skillData.MaxLevel);
                break;
            case SkillLevel.Advanced_b:
                SkillData.Level = Mathf.Clamp(SkillData.Level, 0, skillData.MaxLevel);
                break;
            default:
                break;
        }
        UpdateUpgradeCost();
        UpgradeAttributes();
    }
    public void UpdateUpgradeCost()
    {
        switch (SkillLevel)
        {
            case SkillLevel.Basic:
                SkillData.UpgradeCost = (upgradeCostNumber / 10) * SkillData.Level + (skillData.Level > 0 ? ((SkillData.Level - 1) * (upgradeCostNumber / 4)) : 0);
                break;
            case SkillLevel.Advanced_a:
                SkillData.UpgradeCost = upgradeCostNumber * SkillData.Level;
                break;
            case SkillLevel.Advanced_b:
                SkillData.UpgradeCost = upgradeCostNumber * SkillData.Level;
                break;
            default:
                break;
        }
    }
    public void UpgradeAttributes()
    {
        foreach (Attribute attribute in SkillData.Attributes)
        {
            attribute.Value = upgradeAttributeValueNumber * (SkillData.UpgradeCost/ 100);
        }
    }
    [ContextMenu("UpgradeSkill")]
    public void UpgradeSkill()
    {
        if (SkillData.Level < skillData.MaxLevel)
        {
            SkillData.Level++;
            UpdateUpgradeCost();
            UpgradeAttributes();
            foreach (Attribute attribute in SkillData.Attributes)
            {
                FindObjectOfType<PlayerStats>().ChangeAttributeValue(attribute.AttributeName, attribute.Value);
            }
        }
    }
}
[System.Serializable]
public class SkillData
{
    [SerializeField] private string skillName;
    [SerializeField] private string description;
    [SerializeField] private int level;
    [SerializeField] private int maxLevel;
    [SerializeField] private float upgradeCost;
    [SerializeField] private Sprite sprite;
    [SerializeField] private List<Attribute> attributes;
    public List<Attribute> Attributes { get => attributes; set => attributes = value; }
    public int Level { get => level; set => level = value; }
    public float UpgradeCost { get => upgradeCost; set => upgradeCost = value; }
    public int MaxLevel { get => maxLevel; set => maxLevel = value; }
}
public enum SkillType
{
    Healer, Tank, Fight, Leprechaun, TimeTraveler, Collector, TrapSpecialist
}
public enum SkillLevel
{
    Basic, Advanced_a, Advanced_b
}
