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
}
