using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private List<SkillTrio> skillTrio = new List<SkillTrio>();
    private void Start()
    {
        foreach (var item in skillTrio)
        {
            item.SetSkill();
        }
    }
    public void OnButtonPressed(SkillData skillData,int level)
    {
        foreach (SkillTrio _skill in skillTrio)
        {
            if (_skill.SkillData.SkillName == skillData.SkillName)
            {
                UpgradeSkill(skillData, _skill,level);
                return;
            }
        }
    }
    public void UpgradeSkill(SkillData _skillData,SkillTrio skill, int level)
    {
        if (skill.Level < _skillData.MaxLevel&&level>skill.Level)
        {
            skill.Level = level;
            skill.Cost = _skillData.CalculateCost(skill.SkillAdvancemenLevel, level);
            List<Attribute> newAttributes = _skillData.CalculateAttribute(skill.SkillData,skill.Cost);
            foreach (Attribute attribute in newAttributes)
            {
                gameObject.GetComponent<PlayerStats>().ChangeAttributeValue(attribute.AttributeName, attribute.Value);
            }
        }
    }
}
[System.Serializable]
public class SkillTrio
{
    [SerializeField] private SkillAdvancemenLevel skillAdvancemenLevel;
    [SerializeField] private int level;
    [SerializeField] private int cost;
    [SerializeField] private Skill skill;
    private SkillData skillData;
    public void SetSkill()
    {
        skillData = skill.SkillData.Find(a => a.SkillLevel == skillAdvancemenLevel);
    }
    public SkillAdvancemenLevel SkillAdvancemenLevel { get => skillAdvancemenLevel; set => skillAdvancemenLevel = value; }
    public int Level { get => level; set => level = value; }
    public int Cost { get => cost; set => cost = value; }
    public SkillData SkillData { get => skillData; set => skillData = value; }
    public Skill Skill { get => skill; set => skill = value; }
}
