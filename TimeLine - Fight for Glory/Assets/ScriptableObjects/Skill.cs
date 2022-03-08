using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

[System.Serializable]
[CreateAssetMenu(menuName = "Skill")]
public class Skill : ScriptableObject
{ 
    [SerializeField] private SkillType skillType;
    [SerializeField] private Sprite sprite;
    [SerializeField] private List<SkillData> skillData;

    public SkillType SkillType { get => skillType; set => skillType = value; }
    public List<SkillData> SkillData { get => skillData; set => skillData = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
}
[System.Serializable]
public class SkillData
{
    [SerializeField] private string skillName;
    [SerializeField] private string description;
    private int level;
    [SerializeField] private SkillAdvancemenLevel skillLevel;
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

    [SerializeField] private int upgradeCostNumber = 2000;
    public int CalculateCost(SkillAdvancemenLevel skillAdvancementLevel,int _level)
    {
        switch (skillAdvancementLevel)
        {
            case SkillAdvancemenLevel.Basic:
                return upgradeCostNumber / 10 * _level + (_level > 0 ? (_level - 1) * (upgradeCostNumber / 4) : 0);
            case SkillAdvancemenLevel.Advanced_a:
                return upgradeCostNumber * _level;
            case SkillAdvancemenLevel.Advanced_b:
                return upgradeCostNumber * _level;
            default:
                break;
        }
        return int.MaxValue;
    }
    public List<Attribute> CalculateAttribute(SkillData skill, int cost)
    {
        List<Attribute> tempAttributes = new List<Attribute>();
        for (int i = 0; i < skill.attributes.Count; i++)
        {
            Attribute tempAtt = new Attribute();
            tempAtt.AttributeName = skill.attributes[i].AttributeName;
            tempAtt.Value = skill.attributes[i].Value * (cost / 100);
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
    Basic, Advanced_a, Advanced_b, Automatic
}
