using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemObject/CraftableItemObject")]
public class CraftableItemObject : GenericItemObject
{
    private void OnValidate()
    {
        if (!ItemTypes.Contains(ItemType.Craftable))
            ItemTypes.Add(ItemType.Craftable);
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