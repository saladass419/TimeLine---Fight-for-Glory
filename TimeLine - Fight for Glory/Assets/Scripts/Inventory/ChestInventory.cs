using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChestInventory : GenericInventory
{
    [SerializeField] private List<GenericItemObject> possibleItems;

    [SerializeField] private RarityType rarityType;

    [SerializeField] private bool FillChestOnStart=true;
    [SerializeField] private bool isFull;

    private int amount;

    private System.Random rnd = new System.Random();

    private void Awake()
    {
        inventoryType = InventoryType.ChestInventory;
        if(maxSlot == 0) maxSlot = 12;
    }
    private void Start()
    {
        SetStartingItemsFromInspector();
        FillChest();
    }
    private void FillPossibleAndShuffle()
    {
        possibleItems = new List<GenericItemObject>();
        possibleItems = Database.instance.ItemObjects.Where(a => a.RarityType <= rarityType).ToList();
        StaticImportantFunction.Shuffle(possibleItems);
    }
    [ContextMenu("FillChest")]
    private void FillChest()
    {
        if (!FillChestOnStart) return;
        FillPossibleAndShuffle();
        inventory.Clear();
        inventoryItems.Clear();

        GenericItemObject tempItem = possibleItems.First(a => a.RarityType == rarityType);
        if (tempItem.IsStackable)
        {
            amount = rnd.Next(1, (Mathf.FloorToInt(tempItem.Rarity) / 10));
        }
        else
        {
            amount = 1;
        }
        AddItemToInventory(tempItem, amount);
        possibleItems.Remove(tempItem);

        for (int i = 0; i < possibleItems.Count; i++)
        {
            if (isFull) break;
            if (possibleItems[i].Rarity >= rnd.Next(0, 100))
            {

                if (possibleItems[i].IsStackable)
                {
                    amount = rnd.Next(Mathf.FloorToInt(possibleItems[i].Rarity / 100f), (Mathf.FloorToInt(possibleItems[i].Rarity) / 10));
                }
                else
                {
                    amount = rnd.Next(Mathf.FloorToInt(possibleItems[i].Rarity / 100f), 1);
                }
                isFull=!AddItemToInventory(possibleItems[i], amount);
            }
        }
    }
    public bool PutItemsInChest(GenericInventory inventoryFrom, GenericItemObject item, int amount) // needs a interaction managers of some sort
    {
        if (IsEnoughSpaceInInventory(item,amount)==amount)
        {
            if (inventoryFrom.RemoveItemFromInventory(item, amount))
            {
                AddItemToInventory(item, amount);
                return true;
            }
        }
        return false;
    }
    public bool TakeItemsFronChest(GenericInventory inventoryTo, GenericItemObject item, int amount) // needs a interaction managers of some sort
    {
        if (IsEnougItemsInInventory(item, amount))
        {
            if (inventoryTo.AddItemToInventory(item, amount))
            {
                RemoveItemFromInventory(item, amount);
                return true;
            }
        }
        return false;
    }
}
