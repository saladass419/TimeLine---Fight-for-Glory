using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float range = 10f;

    private GameObject currentlyOpen;

    [SerializeField] private GameObject chestUI;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject equipmentUI;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject informationUI;
    [SerializeField] private GameObject skillUI;

    private float distanceItem;
    private float distanceChest;
    private float distanceShop;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject item = isObjectCloseEnough("Item");
            GameObject chest = isObjectCloseEnough("Chest");
            GameObject shop = isObjectCloseEnough("Shop");

            if (item != null) distanceItem  = Vector3.Distance(item.transform.position, transform.position); 
            else distanceItem = float.MaxValue;

            if (chest != null) distanceChest = Vector3.Distance(chest.transform.position, transform.position);
            else distanceChest = float.MaxValue;

            if (shop != null) distanceShop = Vector3.Distance(shop.transform.position, transform.position);
            else distanceShop = float.MaxValue;

            //Interact with buildings/items
            switch (Mathf.Min(Mathf.Min(distanceItem,distanceShop),distanceChest))
            {
                case var value when value == distanceItem:
                    if (item != null) item.GetComponent<Item>().ItemPickUp();
                break;
                case var value when value == distanceChest:
                    if (currentlyOpen == null||currentlyOpen == chestUI)
                    {
                        if (chest != null)
                        {
                            chestUI.GetComponent<GenericInventoryUI>().Inventory = chest.GetComponent<ChestInventory>();

                            chestUI.SetActive(!chestUI.activeSelf);
                            inventoryUI.SetActive(!inventoryUI.activeSelf);
                            informationUI.SetActive(!informationUI.activeSelf);
                            UIOpen.isAnythingOpen = !UIOpen.isAnythingOpen;

                            if (currentlyOpen == null) currentlyOpen = chestUI;
                            else if (currentlyOpen == chestUI) currentlyOpen = null;
                        }
                    }
                break;
                case var value when value == distanceShop:
                    if (currentlyOpen == null || currentlyOpen == shopUI)
                    {
                        if (shop != null)
                        {
                            shopUI.GetComponent<GenericInventoryUI>().Inventory = shop.GetComponent<GenericInventory>();

                            shopUI.SetActive(!shopUI.activeSelf);
                            inventoryUI.SetActive(!inventoryUI.activeSelf);
                            informationUI.SetActive(!informationUI.activeSelf);
                            UIOpen.isAnythingOpen = !UIOpen.isAnythingOpen;

                            if (currentlyOpen == null) currentlyOpen = shopUI;
                            else if (currentlyOpen == shopUI) currentlyOpen = null;
                        }
                    }
                break;
            }
        }
        //Open inventory
        if(Input.GetKeyDown(KeyCode.E)&&(currentlyOpen == null || currentlyOpen == equipmentUI))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            equipmentUI.SetActive(!equipmentUI.activeSelf);
            informationUI.SetActive(!informationUI.activeSelf);
            UIOpen.isAnythingOpen = !UIOpen.isAnythingOpen;

            if (currentlyOpen == null) currentlyOpen = equipmentUI;
            else if (currentlyOpen == equipmentUI) currentlyOpen = null;
        }

        //Open SkillTree
        if(Input.GetKeyDown(KeyCode.Y))
        {
            skillUI.SetActive(!skillUI.activeSelf);
            skillUI.GetComponent<SkillUI>().Player = gameObject;
            UIOpen.isAnythingOpen = !UIOpen.isAnythingOpen;
        }
    }

    private GameObject isObjectCloseEnough(string tag)
    {
        float minDistance = float.MaxValue;
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
        float distance = 0f;
        GameObject objectToReturn = null;

        foreach(GameObject GO in objs)
        {
            distance = Vector3.Distance(transform.position, GO.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                objectToReturn = GO;
            }
        }

        if(distance < range)
        {
            return objectToReturn;
        }
        return null;
    }
}
