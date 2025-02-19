using System;
using System.Collections.Generic;
using UnityEngine;

public class Shop : A_ShopManager
{
    List<Tuple<string, int>> towersAvailable = new List<Tuple<string, int>>();

    private void Start()
    {
        towersAvailable.Add(new Tuple<string, int>("Turret 1a", 20));
        towersAvailable.Add(new Tuple<string, int>("Turret 1b", 40));
        towersAvailable.Add(new Tuple<string, int>("Turret 1c", 60));
        towersAvailable.Add(new Tuple<string, int>("Turret 1d", 80));
    }

    public override A_Tower buyTower(string towerName)
    {
        throw new NotImplementedException();
    }

    public override A_Tower buyIfPlayerCanAffordIt(int playerMoney, string towerName)
    {
        foreach (Tuple<string, int> tower in towersAvailable)
        {
            if (tower.Item1 == towerName)
            {
                if (playerMoney >= tower.Item2)
                {
                    mediator.onEventFromManagers(new Tuple<string, object>("REMOVE_MONEY", tower.Item2));
                    return towerFactory.createTower(tower.Item1);
                }

                mediator.onEventFromManagers(new Tuple<string, object>("Pas assez d'argent!", null));
            }
        }

        throw new Exception("tower not found in towers available");
    }
}