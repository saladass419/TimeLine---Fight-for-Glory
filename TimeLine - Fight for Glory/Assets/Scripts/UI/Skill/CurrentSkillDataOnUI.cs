using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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
                gameObject.GetComponent<Image>().sprite = data.Sprite[level - 1];
            }
        }
    }
    public void OnButtonClicked()
    {
        UIManager.instance.SkillInformationUI.SetActive(true);

        if (onButtonClicked != null)
        {
            onButtonClicked.Invoke(skillData, level);
            Debug.Log("asfasfasf");
        }
    }
}
