using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GridGameUIManager : MonoBehaviour
{
    [SerializeField] private Sprite CloseButtonSprite;
    [SerializeField] private Sprite OpenButtonSprite;

    [SerializeField] private Button closePanelButton;
    [SerializeField] private GameObject informationPanel;

    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text heroType;
    [SerializeField] private TMP_Text attackType;
    [SerializeField] private TMP_Text rangeType;

    public void CloseInformationPanel()
    {
        informationPanel.SetActive(!informationPanel.activeSelf);
        if(informationPanel.activeSelf == true)
        {
            closePanelButton.image.sprite = CloseButtonSprite;
        }
        else
        {
            closePanelButton.image.sprite = OpenButtonSprite;
        }
    }

    private void EnableUI()
    {

    }

    private void DisableUI()
    {

    }

    public void RefreshUI(HeroCard card)
    {
        cardName.text = card.CardName;
        description.text = card.Description;
        heroType.text = card.HeroCardType.ToString();
        attackType.text = card.AttackType.ToString();
        rangeType.text = card.RangeType.ToString();
    }
}
