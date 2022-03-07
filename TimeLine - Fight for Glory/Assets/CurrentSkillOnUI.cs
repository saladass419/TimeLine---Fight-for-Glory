using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using TMPro;

public class CurrentSkillOnUI : MonoBehaviour
{
    public event Action<Skill> onButtonClicked;

    [SerializeField] private Skill skill;
    private void Awake()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = skill.SkillType.ToString();
    }
    public void OnButtonClicked()
    {
        if (onButtonClicked != null)
            onButtonClicked.Invoke(skill);
    }
}
