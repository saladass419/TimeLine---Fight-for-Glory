using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Database : MonoBehaviour,ISerializationCallbackReceiver
{
    public static Database instance;
    private void Awake()
    {
        instance = this;
    }
    public List<GenericItemObject> ItemObjects;
    public List<GenericItemObject> itemsUnlockedInCurrentDimension;
    public List<GenericItemObject> itemsUnlocked;

    [ContextMenu("Update ID's")]
    public void UpdateID()
    {
        for (int i = 0; i < ItemObjects.Count; i++)
        {
            if (ItemObjects[i].ItemId != i)
                ItemObjects[i].ItemId = i;
        }
    }
    public void OnAfterDeserialize()
    {
        UpdateID();
    }
    public void OnBeforeSerialize()
    {
        UpdateID();
    }
    public void ItemUnlockedInCurrentDimension(int dimensionNumber)
    {
        itemsUnlocked = new List<GenericItemObject>().Where(x => x.ItemDimensionAvailable == dimensionNumber).ToList();
        AddUnlockedItemsToList();
    }
    public void AddUnlockedItemsToList()
    {
        foreach (GenericItemObject item in itemsUnlockedInCurrentDimension)
        {
            itemsUnlocked.Add(item);
        }
    }
}
