using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float range = 10f;
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
                    UIManager.instance.SetChest(chest,gameObject);
                break;
                case var value when value == distanceShop:
                    UIManager.instance.SetShop(shop,gameObject);
                    break;
            }
        }
        //Open inventory
        if(Input.GetKeyDown(KeyCode.E))
        {
            UIManager.instance.SetEquipment(gameObject);
        }

        //Open SkillTree
        if(Input.GetKeyDown(KeyCode.Y))
        {
            UIManager.instance.SetSkillTree(gameObject);
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
