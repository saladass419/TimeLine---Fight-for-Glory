using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class CardFactory
{
    private static System.Random rnd = new System.Random();
    private static List<GameObject> heroes = new List<GameObject>();
    private static List<GameObject> spells = new List<GameObject>();
    private static List<GameObject> items = new List<GameObject>();
    private static bool isInitialized = false;
    private static Dictionary<string, List<Position>> possibleAttackPatterns = new Dictionary<string, List<Position>>();
    private static Dictionary<string, List<Position>> possibleMovePatterns = new Dictionary<string, List<Position>>();
    private static void InitializeFactory()
    {
        if (isInitialized)
            return;

        isInitialized = true;

        possibleAttackPatterns.Add("shortRange", new List<Position>()  { new Position(0,1), new Position(0, 2)});
        possibleAttackPatterns.Add("shortRange+", new List<Position>() { new Position(0, 1), new Position(0, 2), new Position(0, 3) });
        possibleAttackPatterns.Add("shortRange++", new List<Position>() { new Position(0, 1), new Position(0, 2), new Position(1, 2), new Position(-2, -2) });

        possibleMovePatterns.Add("shortRange", new List<Position>() { new Position(0, 1), new Position(0, 2) });
        possibleMovePatterns.Add("shortRange+", new List<Position>() { new Position(0, 1), new Position(0, 2), new Position(3, 3), new Position(-1, -1) });
        possibleMovePatterns.Add("shortRange++", new List<Position>() { new Position(0, 1), new Position(0, 2), new Position(-2, 2), new Position(2, -2) });


        var objects = Resources.LoadAll("Dragons", typeof(GameObject)).Cast<GameObject>().ToArray();
        foreach (var t in objects)
            heroes.Add(t);
        objects = Resources.LoadAll("Items", typeof(GameObject)).Cast<GameObject>().ToArray();
        foreach (var t in objects)
            items.Add(t);
        //objects = Resources.LoadAll("Spells", typeof(GameObject)).Cast<GameObject>().ToArray();
        //foreach (var t in objects)
        //    spells.Add(t);

    }



    //Card will be random if randomCard = 0, if 1 it will be HeroCard, 2 SpellCard, 3 ItemCard
    public static GameObject CreateCard(int randomCard)
    {
        InitializeFactory();

        if (randomCard == 0)
        {
            randomCard = rnd.Next(1, 3);
        }


        switch (randomCard)
        {
            case 1:
                int number = rnd.Next(0, heroes.Count);
                giveRandomStatsToHeroCard(heroes[number]);
                return heroes[number];
            case 2:
                //number = rnd.Next(0, spells.Count);
                //return spells[number];
                return null;
            case 3:
                number = rnd.Next(0, items.Count);
                return items[number];
            //break;
            default:
                return null;
        }
    }

    public static void giveRandomStatsToHeroCard(GameObject hero)
    {
        int number = 0;

        Array attackTypes = Enum.GetValues(typeof(AttackType));
        number = rnd.Next(1, attackTypes.Length);
        hero.GetComponent<HeroCard>().AttackType = (AttackType)number;

        Array rangeTypes = Enum.GetValues(typeof(RangeType));
        number = rnd.Next(1, rangeTypes.Length);
        hero.GetComponent<HeroCard>().RangeType = (RangeType)number;

        hero.GetComponent<HeroCard>().TilesToAttack = new List<Position>();

        foreach(Position position in possibleAttackPatterns["shortRange"])
        {
            hero.GetComponent<HeroCard>().TilesToAttack.Add(position);
        }

        hero.GetComponent<HeroCard>().TilesToMove = new List<Position>();
        hero.GetComponent<HeroCard>().ToBeAdded = new List<Position>();

        foreach (Position position in possibleMovePatterns["shortRange++"])
        {
            hero.GetComponent<HeroCard>().ToBeAdded.Add(position);
        }
    }
}
