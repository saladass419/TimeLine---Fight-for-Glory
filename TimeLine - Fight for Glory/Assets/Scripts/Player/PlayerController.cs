using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float threshhold = 10f;
    [SerializeField] private InventoryUIManager inventoryToOpen;

    void Update()
    {
        //Open inventory
        if(Input.GetKeyDown(KeyCode.E))
        {
            inventoryToOpen.OpenInventory(gameObject);
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
                inventoryToOpen.OpenInventory(shop);
            }
        }

        //Open Chest
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameObject chest = isObjectCloseEnough("Chest");
            if (chest != null)
            {
                inventoryToOpen.OpenInventory(chest);
            }
        }

    }

    private GameObject isObjectCloseEnough(string tag)
    {
        float minDistance = 100000000f;
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
