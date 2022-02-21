using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private List<Skill> skills;

    public event Action<Skill, SkillLevel> upgradeSkillLevel;
    public void OnButtonPressed(Skill skill, SkillLevel skillLevel, List<Attribute> attributes)
    {
        if (upgradeSkillLevel != null)
        {
            upgradeSkillLevel(skill, skillLevel);
        }

        foreach (Attribute attribute in attributes)
        {
            FindObjectOfType<PlayerStats>().ChangeAttributeValue(attribute.AttributeName, attribute.Value);
        }
    }
}
