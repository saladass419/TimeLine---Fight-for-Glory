using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    [SerializeField] private float level;
    [SerializeField] private List<Attribute> attributes;
    [SerializeField] private List<Skill> skills; // redo

    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private PlayerEquipmentInventory equipments;
    public float Level { get => level; set => level = value; }
    public List<Attribute> Attributes { get => attributes; set => attributes = value; }
    public List<Skill> Skills { get => skills; set => skills = value; }
    public PlayerInventory Inventory { get => inventory; set => inventory = value; }
    public PlayerEquipmentInventory Equipments { get => equipments; set => equipments = value; }
}
