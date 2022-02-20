using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerCrafting : MonoBehaviour
{
    private PlayerInventory inventory;
    private void Start()
    {
        inventory = gameObject.GetComponent<PlayerInventory>();
    }
    public void CraftItem(CraftableItemObject item)
    {
        bool isEnough = true;
        for (int i = 0; i < item.CraftingRecipe.Length; i++)
        {
            if (!inventory.IsEnougItemsInInventory(item.CraftingRecipe[i].item, item.CraftingRecipe[i].amount))
            {
                isEnough = false;
                break;
            }
        }
        if (isEnough)
        {
            if (!inventory.AddItemToInventory(item, 1)) return;
            for (int i = 0; i < item.CraftingRecipe.Length; i++)
            {
                inventory.RemoveItemFromInventory(item.CraftingRecipe[i].item, item.CraftingRecipe[i].amount);
            }
        }
        else
        {
            Debug.Log("Not enough items in inventory! köcsög");
        }
    }
}
