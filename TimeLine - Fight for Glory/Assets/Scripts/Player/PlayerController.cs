using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float trashhold = 10f;

    void Update()
    {
        //Open inventory
        if(Input.GetKeyDown(KeyCode.E))
        {

        }

        //Open SkillTree
        if(Input.GetKeyDown(KeyCode.Y))
        {

        }

        //Open Shop
        if (Input.GetKeyDown(KeyCode.H))
        {
            GameObject shop = isShopCloseEnough("Shop");
            if(shop != null)
            {
                //Megnyitni a shophoz tartozó inventoryt - bezárni azt
            }
        }

        //Open Chest
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameObject chest = isShopCloseEnough("Chest");
            if (chest != null)
            {
                //Megnyitni a chesthez tartozó inventoryt - bezárni azt
            }
        }

    }

    private GameObject isShopCloseEnough(string tag)
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

        if(distance < trashhold)
        {
            return objectToReturn;
        }
        return null;
    }
}
