using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building
{
    private int currentLevel = 1;
    private int maxLevel = 10;
    private List<int> upgradeCosts;
    private List<List<Resource>> resourceCosts;

    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public int MaxLevel { get => maxLevel; set => maxLevel = value; }
    public List<int> UpgradeCost { get => upgradeCosts; set => upgradeCosts = value; }

    private void upgradeBuilding() 
    {
        //Check if player has enough money
        CurrentLevel++;
    }



}
