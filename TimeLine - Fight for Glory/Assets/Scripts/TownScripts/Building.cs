using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    private int currentLevel = 1;
    private int maxLevel = 10;

    private List<int> upgradeCosts;
    private Dictionary<Resource,int> resourceCosts;

    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public int MaxLevel { get => maxLevel; set => maxLevel = value; }
    public List<int> UpgradeCost { get => upgradeCosts; set => upgradeCosts = value; }
    public Dictionary<Resource, int> ResourceCosts { get => resourceCosts; set => resourceCosts = value; }
    private void UpgradeBuilding() 
    {
        //Check if player has enough money
        CurrentLevel++;
    }
}
