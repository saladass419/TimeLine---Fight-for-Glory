using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private int money;
    [SerializeField] private Dictionary<Resource, int> resources;
    [SerializeField] private GenericInventory inventory;
    [SerializeField] private List<Hero> heroes;
    public int Level { get => level; set => level = value; }
    public int Money { get => money; set => money = value; }
    public Dictionary<Resource, int> Resources { get => resources; set => resources = value; }
    public GenericInventory Inventory { get => inventory; set => inventory = value; }
    public List<Hero> Heroes { get => heroes; set => heroes = value; }
}
