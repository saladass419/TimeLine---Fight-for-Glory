using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemObject/CraftableItemObject")]
public class CraftableItemObject : GenericItemObject
{
    private void OnValidate()
    {
        if (!itemTypes.Contains(ItemType.Craftable))
            itemTypes.Add(ItemType.Craftable);
    }
    [SerializeField] private CraftingRecipe[] craftingRecipe;
    public CraftingRecipe[] CraftingRecipe { get => craftingRecipe; set => craftingRecipe = value; }
}
[System.Serializable]
public class CraftingRecipe
{
    public GenericItemObject item;
    public int amount;
}