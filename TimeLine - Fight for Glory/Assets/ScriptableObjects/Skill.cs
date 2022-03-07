using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

[System.Serializable]
[CreateAssetMenu(menuName = "Skill")]
public class Skill : ScriptableObject
{
    [SerializeField] private int skillID;

    [SerializeField] private SkillType skillType;
    [SerializeField] private List<SkillData> skillData;

    public SkillType SkillType { get => skillType; set => skillType = value; }
    public List<SkillData> SkillData { get => skillData; set => skillData = value; }
    public int SkillID { get => skillID; set => skillID = value; }
}
[System.Serializable]
public class SkillData
{
    [SerializeField] private int dataID;
    [SerializeField] private string skillName;
    [SerializeField] private string description;
    [SerializeField] private SkillAdvancemenLevel skillLevel;
    [SerializeField] private int level;
    [SerializeField] private int maxLevel;
    [SerializeField] private float upgradeCost;
    [SerializeField] private Sprite sprite;
    [SerializeField] private List<Attribute> attributes;
    public List<Attribute> Attributes { get => attributes; set => attributes = value; }
    public int Level { get => level; set => level = value; }
    public float UpgradeCost { get => upgradeCost; set => upgradeCost = value; }
    public int MaxLevel { get => maxLevel; set => maxLevel = value; }
    public SkillAdvancemenLevel SkillLevel { get => skillLevel; set => skillLevel = value; }
    public string SkillName { get => skillName; set => skillName = value; }
    public string Description { get => description; set => description = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public int DataID { get => dataID; set => dataID = value; }

    [SerializeField] private int upgradeCostNumber = 2000;
    [SerializeField] private int upgradeAttributeValueNumber = 20;
    public int CalculateCost(SkillAdvancemenLevel skillAdvancementLevel, int level)
    {
        switch (skillAdvancementLevel)
        {
            case SkillAdvancemenLevel.Basic:
                return upgradeCostNumber / 10 * level + (level > 0 ? (level - 1) * (upgradeCostNumber / 4) : 0);
            case SkillAdvancemenLevel.Advanced_a:
                return upgradeCostNumber * level;
            case SkillAdvancemenLevel.Advanced_b:
                return upgradeCostNumber * level;
        }
        return int.MaxValue;
    }
    public List<Attribute> CalculateAttribute(SkillData skill, int cost)
    {
        List<Attribute> tempAttributes = new List<Attribute>();
        for (int i = 0; i < skill.attributes.Count; i++)
        {
            Attribute tempAtt = new Attribute();
            tempAtt.Value = upgradeAttributeValueNumber * (cost / 100);
            tempAttributes.Add(tempAtt);
        }
        return tempAttributes;
    }
}
public enum SkillType
{
    Healer, Tank, Fight, Leprechaun, TimeTraveler, Collector, TrapSpecialist
}
public enum SkillAdvancemenLevel
{
    Basic, Advanced_a, Advanced_b
}
