using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class CurrentSkillDataOnUI : MonoBehaviour
{
    public event Action<SkillData,int> onButtonClicked;

    [SerializeField] private SkillAdvancemenLevel skillAdvancemenLevel;
    [SerializeField] private int level;
    [SerializeField] private SkillData skillData;
    private void OnValidate()
    {
        switch (skillAdvancemenLevel)
        {
            case SkillAdvancemenLevel.Basic:
                level = Mathf.Clamp(level, 1, 5);
                break;
            case SkillAdvancemenLevel.Advanced_a:
                level = Mathf.Clamp(level, 1, 2);
                break;
            case SkillAdvancemenLevel.Advanced_b:
                level = Mathf.Clamp(level, 1, 2);
                break;
            default:
                break;
        }
    }
    private void Start()
    {
        foreach (var item in FindObjectsOfType<CurrentSkillOnUI>())
        {
            item.onButtonClicked += SetSkillData;
        }
    }
    private void SetSkillData(Skill skill)
    {
        foreach (var data in skill.SkillData)
        {
            if (data.SkillLevel == skillAdvancemenLevel)
            {
                skillData = data;
            }
        }
    }
    public void OnButtonClicked()
    {
        if (onButtonClicked != null)
            onButtonClicked.Invoke(skillData, level);
    }
}
