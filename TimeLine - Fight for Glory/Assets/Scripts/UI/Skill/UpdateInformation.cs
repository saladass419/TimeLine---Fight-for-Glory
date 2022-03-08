using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateInformation : MonoBehaviour
{
    private Image image;
    private TextMeshProUGUI nameTxt;
    private TextMeshProUGUI descText;
    private void Start()
    {
        image = GetComponentsInChildren<Image>()[1];
        nameTxt = GetComponentsInChildren<TextMeshProUGUI>()[0];
        descText = GetComponentsInChildren<TextMeshProUGUI>()[1];
        foreach (var item in FindObjectsOfType<CurrentSkillDataOnUI>())
        {
            item.onButtonClicked += UpdateInformationInUI;
        }
    }
    private void UpdateInformationInUI(SkillData skill, int level)
    {
        image.sprite = skill.Sprite;
        nameTxt.text = skill.SkillName + " - " + level;
        descText.text = skill.Description;
    }
}
