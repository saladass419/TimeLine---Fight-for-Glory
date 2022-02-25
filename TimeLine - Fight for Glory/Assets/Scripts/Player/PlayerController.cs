using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    private float threshhold = 10f;

    [SerializeField] private GameObject chestUI;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject equipmentUI;
    [SerializeField] private GameObject informationUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject item = isObjectCloseEnough("Item");
            if (item != null)
            {
                item.GetComponent<Item>().ItemPickUp();
            }
        }
        //Open inventory
        if(Input.GetKeyDown(KeyCode.E)&&!chestUI.activeInHierarchy)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            equipmentUI.SetActive(!equipmentUI.activeSelf);
            informationUI.SetActive(!informationUI.activeSelf);
        }

        //Open SkillTree
        if(Input.GetKeyDown(KeyCode.Y))
        {

        }

        //Open Shop
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameObject shop = isObjectCloseEnough("Shop");
            if(shop != null)
            {
                //inventoryToOpen.OpenInventory(shop);
            }
        }

        //Open Chest
        if (Input.GetKeyDown(KeyCode.L)&&!equipmentUI.activeInHierarchy)
        {
            GameObject chest = isObjectCloseEnough("Chest");
            if (chest != null)
            {
                chestUI.GetComponent<InventoryUIManager>().OpenInventory(chest.GetComponent<ChestInventory>());

                chestUI.SetActive(!chestUI.activeSelf);
                inventoryUI.SetActive(!inventoryUI.activeSelf);
                informationUI.SetActive(!informationUI.activeSelf);
            }
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

        if(distance < threshhold)
        {
            return objectToReturn;
        }
        return null;
    }
}
