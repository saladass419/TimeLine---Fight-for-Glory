using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "ItemObject/GenericItemObject")]
public class GenericItemObject : ScriptableObject
{
    [SerializeField] private int itemId;
    [SerializeField] private int itemDimensionAvailable;
    [SerializeField] private bool isItemUnlocked;
    [SerializeField] [Range(0,100)] private int rarity;
    [SerializeField] private RarityType rarityType;
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] public List <ItemType> itemTypes;
    [SerializeField] private EquipmentType equipmentType;
    [SerializeField] private int price;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private bool isStackable;
    [SerializeField] private Attribute[] attributes;
    [SerializeField] private Sprite backGround;
    public EquipmentType EquipmentType { get => equipmentType; set => equipmentType = value; }
    public int ItemId { get => itemId; set => itemId = value; }
    public bool IsItemUnlocked { get => isItemUnlocked; set => isItemUnlocked = value; }
    public Attribute[] Attributes { get => attributes; set => attributes = value; }
    public int ItemDimensionAvailable { get => itemDimensionAvailable; set => itemDimensionAvailable = value; }
    public int Rarity { get => rarity; set => rarity = value; }
    public bool IsStackable { get => isStackable; set => isStackable = value; }
    public int Price { get => price; set => price = value; }
    public RarityType RarityType { get => rarityType; private set => rarityType = value; }
    public Sprite ItemSprite { get => itemSprite; set => itemSprite = value; }
    public string ItemName { get => itemName; set => itemName = value; }
    public string ItemDescription { get => itemDescription; set => itemDescription = value; }
    public Sprite BackGround { get => backGround; set => backGround = value; }

    private void OnValidate()
    {
        switch (rarity)
        {
            case var expression when rarity > 75:
                RarityType = RarityType.Common;
                break;
            case var expression when (rarity <= 75 && rarity > 50):
                RarityType = RarityType.Uncommon;
                break;
            case var expression when (rarity <= 50 && rarity > 25):
                RarityType = RarityType.Rare;
                break;
            case var expression when (rarity <= 25 && rarity > 10):
                RarityType = RarityType.Epic;
                break;
            case var expression when (rarity <= 10 && rarity > 0):
                RarityType = RarityType.Legendary;
                break;
            case var expression when (rarity <= 0):
                RarityType = RarityType.CantBeFound;
                break;
        }
    }
}
public enum ItemType
{
    Generic, Craftable, Shop, Equipment
}
public enum RarityType
{
    Common, Uncommon, Rare, Epic, Legendary, CantBeFound
}
