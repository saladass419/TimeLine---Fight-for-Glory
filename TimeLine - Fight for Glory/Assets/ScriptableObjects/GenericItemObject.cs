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
    [SerializeField] private int rarity;
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] public List <ItemType> itemTypes;
    [SerializeField] private int price;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private bool isStackable;
    [SerializeField] private Attribute[] attributes;
    public int ItemId { get => itemId; set => itemId = value; }
    public bool IsItemUnlocked { get => isItemUnlocked; set => isItemUnlocked = value; }
    public Attribute[] Attributes { get => attributes; set => attributes = value; }
    public int ItemDimensionAvailable { get => itemDimensionAvailable; set => itemDimensionAvailable = value; }
    public int Rarity { get => rarity; set => rarity = value; }
    public bool IsStackable { get => isStackable; set => isStackable = value; }
    public int Price { get => price; set => price = value; }
}
public enum ItemType
{
    Generic, Craftable, Shop
}
