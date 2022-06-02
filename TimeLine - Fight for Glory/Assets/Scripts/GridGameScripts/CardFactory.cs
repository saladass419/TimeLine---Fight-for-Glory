using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class CardFactory
{
    private static System.Random rnd = new System.Random();
    private static Dictionary<string, Type> heroesList = new Dictionary<string, Type>();
    private static bool isInitialized = false;
    private static Dictionary<string, List<(int PosX, int PosY)>> possibleAttackPatterns = new Dictionary<string, List<(int PosX, int PosY)>>();
    private static Dictionary<string, List<(int PosX, int PosY)>> possibleMovePatterns = new Dictionary<string, List<(int PosX, int PosY)>>();
    private static void InitializeFactory()
    {
        if (isInitialized)
            return;

        isInitialized = true;


        possibleAttackPatterns.Add("shortRange", new List<(int, int)>() {(0,1)});
        possibleAttackPatterns.Add("shortRange+", new List<(int, int)>() {(0,1), (0,2)});
        possibleAttackPatterns.Add("shortRange++", new List<(int, int)>() {(0,1), (0,2), (0,3)});

        possibleMovePatterns.Add("shortRange", new List<(int, int)>() {(0,1)});
        possibleMovePatterns.Add("shortRange+", new List<(int, int)>() {(0,1), (0,2)});
        possibleMovePatterns.Add("shortRange++", new List<(int, int)>() {(0, 1), (0,2), (0,3), (1,0)});

        var heroTypes = Assembly.GetAssembly(typeof(Card)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Card)));

        foreach(var type in heroTypes)
        {
            var tempHeroType = Activator.CreateInstance(type) as Card;
            heroesList.Add(tempHeroType.CardName, type);
        }
    }

    private static Card GetCard(string cardName)
    {
        if (heroesList.ContainsKey(cardName))
        {
            Type type = heroesList[cardName];
            var card = Activator.CreateInstance(type) as Card;
            return card;
        }
        else
        {
            Debug.Log("Not valid CardName: " + cardName);
            return null;
        }
    }


    //Card will be random if randomCard = 0, if 1 it will be HeroCard, 2 SpellCard, 3 ItemCard
    public static Card CreateCard(int randomCard)
    {
        InitializeFactory();

        Card card = null;

        if (randomCard == 0)
        {
            randomCard = rnd.Next(1, 3);
        }

        switch (randomCard)
        {
            case 1:
                card = GetCard("HeroCard");
                giveRandomStatsToHeroCard((HeroCard)card);
                break;
            case 2:
                card = GetCard("SpellCard");
                //Debug.Log("SpellCard created!");
                break;
            case 3:
                card = GetCard("ItemCard");
                //Debug.Log("ItemCard created!");
                break;
        }

        return card;
    }

    public static void giveRandomStatsToHeroCard(HeroCard hero)
    {
        hero.Description = "This heo is a close damage monster";

        Array heroCardTypes = Enum.GetValues(typeof(HeroCardType));
        int number = rnd.Next(1, heroCardTypes.Length);
        hero.HeroCardType = (HeroCardType)number;

        Array attackTypes = Enum.GetValues(typeof(AttackType));
        number = rnd.Next(1, attackTypes.Length);
        hero.AttackType = (AttackType)number;

        Array rangeTypes = Enum.GetValues(typeof(RangeType));
        number = rnd.Next(1, rangeTypes.Length);
        hero.RangeType = (RangeType)number;

        hero.TilesToAttack = possibleAttackPatterns["shortRange"];

        hero.TilesToMove = possibleMovePatterns["shortRange++"];
    }
}
