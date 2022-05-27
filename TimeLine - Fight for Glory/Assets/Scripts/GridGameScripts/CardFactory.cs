using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class CardFactory
{
    private static System.Random rnd = new System.Random();
    private static Dictionary<string, Type> heroesList;
    private static bool isInitialized => heroesList != null;

    private static void InitializeFactory()
    {
        if (isInitialized)
            return;

        var heroTypes = Assembly.GetAssembly(typeof(HeroCard)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(HeroCard)));

        heroesList = new Dictionary<string, Type>();

        foreach(var type in heroTypes)
        {
            var tempHeroType = Activator.CreateInstance(type) as HeroCard;
            //heroesList.Add(tempHeroType.Name, type);
        }
    }

    public static HeroCard CreateCard()
    {
        InitializeFactory();

        int numbe = rnd.Next(10);
        return new HeroCard();
    }
}
