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

    private void RefreshUI(HeroCard card)
    {

    }
}
