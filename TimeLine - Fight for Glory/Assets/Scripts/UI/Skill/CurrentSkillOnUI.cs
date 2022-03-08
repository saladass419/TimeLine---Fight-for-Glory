using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class CurrentSkillOnUI : MonoBehaviour
{
    public event Action<Skill> onButtonClicked;

    [SerializeField] private Skill skill;
    private void OnValidate()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = skill.SkillType.ToString();
        gameObject.GetComponent<Image>().sprite = skill.Sprite;
    }
    public void OnButtonClicked()
    {
        if (onButtonClicked != null)
            onButtonClicked.Invoke(skill);
    }
}
