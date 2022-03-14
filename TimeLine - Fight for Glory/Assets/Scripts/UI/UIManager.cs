using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] private CinemachineFreeLook freeLook;
    [SerializeField] private GameObject chestUI;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject equipmentUI;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject informationUI;
    [SerializeField] private GameObject skillUI;

    private GameObject currentlyOpen;
    private void Start()
    {
        currentlyOpen = null;
    }
    public bool IsAnythingOpenInUI()
    {
        if (currentlyOpen==null) 
            return false;
        return true;
    }

    private void OpenCloseUIElement(GameObject uiObject)
    {
        if (uiObject == null) return;

        if (currentlyOpen == null || currentlyOpen == uiObject)
        {
            currentlyOpen = uiObject;
            currentlyOpen.SetActive(!currentlyOpen.activeSelf);
        }
        else
        {
            currentlyOpen.SetActive(!currentlyOpen.activeSelf);
            currentlyOpen = uiObject;
            currentlyOpen.SetActive(!currentlyOpen.activeSelf);
        }

        inventoryUI.SetActive(true);
        informationUI.SetActive(true);

        freeLook.enabled = false;

        if (!currentlyOpen.activeInHierarchy)
        {
            currentlyOpen = null;
            inventoryUI.SetActive(false);
            informationUI.SetActive(false);

            freeLook.enabled = true;
        }
    }
    private void SetPlayer(GameObject player)
    {
        if (player == null) return;

        inventoryUI.GetComponent<GenericInventoryUI>().Inventory = player.GetComponent<PlayerInventory>();
    }
    public void SetChest(GameObject chest,GameObject player)
    {
        if (chest == null) return;

        SetPlayer(player);

        chestUI.GetComponent<GenericInventoryUI>().Inventory = chest.GetComponent<ChestInventory>();
        OpenCloseUIElement(chestUI);
    }
    public void SetShop(GameObject shop, GameObject player)
    {
        if (shop == null) return;

        SetPlayer(player);

        shopUI.GetComponent<GenericInventoryUI>().Inventory = shop.GetComponent<ShopInventory>();
        OpenCloseUIElement(shopUI);
    }
    public void SetEquipment(GameObject player)
    {
        if (player == null) return;

        SetPlayer(player);

        equipmentUI.GetComponent<GenericInventoryUI>().Inventory = player.GetComponent<PlayerEquipmentInventory>();
        OpenCloseUIElement(equipmentUI);
    }
    public void SetInformation(GameObject item)
    {
        if (item == null)
        {
            informationUI.GetComponentsInChildren<TextMeshProUGUI>()[0].text = null;
            informationUI.GetComponentsInChildren<TextMeshProUGUI>()[1].text = null;
        }
        else
        {
            informationUI.GetComponentsInChildren<TextMeshProUGUI>()[0].text = item.GetComponent<ItemInUI>().Item.ItemName;
            informationUI.GetComponentsInChildren<TextMeshProUGUI>()[1].text = item.GetComponent<ItemInUI>().Item.ItemDescription;
        }
    }
    public void SetSkillTree(GameObject player)
    {
        if (player == null) return;

        skillUI.GetComponent<SkillUI>().Player = player;
        skillUI.SetActive(!skillUI.activeSelf);
    }
}
