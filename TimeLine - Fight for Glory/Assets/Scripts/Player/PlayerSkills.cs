using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private List<SkillCombo> skillCombo = new List<SkillCombo>();
    private void Start()
    {
        foreach (var combs in skillCombo)
        {
            combs.SetSkill();
        }
    }
    public void OnButtonPressed(SkillData skillData,int level)
    {
        foreach (SkillCombo combs in skillCombo)
        {
            foreach (SkillTrio _skill in combs.SkillTrio)
            {
                if (_skill.SkillData.SkillName == skillData.SkillName)
                {
                    UpgradeSkill(skillData, _skill, level);
                    return;
                }
            }
        }
    }
    public void UpgradeSkill(SkillData _skillData,SkillTrio skill, int level)
    {
        if (skill.Level < _skillData.MaxLevel&&level>skill.Level&&(skill.Level+1==level))
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
    private SkillData skillData;
    public SkillAdvancemenLevel SkillAdvancemenLevel { get => skillAdvancemenLevel; set => skillAdvancemenLevel = value; }
    public int Level { get => level; set => level = value; }
    public int Cost { get => cost; set => cost = value; }
    public SkillData SkillData { get => skillData; set => skillData = value; }
}
[System.Serializable]
public class SkillCombo
{
    [SerializeField] private Skill skill;
    [SerializeField] private List<SkillTrio> skillTrio = new List<SkillTrio>();
    public List<SkillTrio> SkillTrio { get => skillTrio; set => skillTrio = value; }
    public Skill Skill { get => skill; set => skill = value; }
    public void SetSkill()
    {
        for (int i = 0; i < skillTrio.Count; i++)
        {
            switch (i)
            {
                case 0:
                    if(skillTrio[i].SkillAdvancemenLevel == SkillAdvancemenLevel.Automatic) skillTrio[i].SkillAdvancemenLevel = SkillAdvancemenLevel.Basic;
                    break;
                case 1:
                    if (skillTrio[i].SkillAdvancemenLevel == SkillAdvancemenLevel.Automatic) skillTrio[i].SkillAdvancemenLevel = SkillAdvancemenLevel.Advanced_a;
                    break;
                case 2:
                    if (skillTrio[i].SkillAdvancemenLevel == SkillAdvancemenLevel.Automatic) skillTrio[i].SkillAdvancemenLevel = SkillAdvancemenLevel.Advanced_b;
                    break;
            }
            skillTrio[i].SkillData = skill.SkillData.Find(a => a.SkillLevel == skillTrio[i].SkillAdvancemenLevel);
        }
    }
}
