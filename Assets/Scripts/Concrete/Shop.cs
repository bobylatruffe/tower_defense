using System;
using System.Collections.Generic;
using UnityEngine;

public class Shop : A_ShopManager
{
    List<Tuple<string, int>> towersAvailable = new List<Tuple<string, int>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Mediator = GameManager.Instance;
        TowerFactory = FindFirstObjectByType<TowerFactory>();

        towersAvailable.Add(new Tuple<string, int>("Turret 1a", 20));
        towersAvailable.Add(new Tuple<string, int>("Turret 2a", 40));
        towersAvailable.Add(new Tuple<string, int>("Turret 3a", 60));
        towersAvailable.Add(new Tuple<string, int>("Turret 4a", 80));
        towersAvailable.Add(new Tuple<string, int>("Turret 5a", 100));
        towersAvailable.Add(new Tuple<string, int>("Turret 6a", 120));
        towersAvailable.Add(new Tuple<string, int>("Turret 7a", 140));
        towersAvailable.Add(new Tuple<string, int>("Turret 8a", 160));
    }

    public override A_Tower buyIfPlayerCanAffordIt(int playerMoney, string towerName)
    {
        foreach (Tuple<string, int> tower in towersAvailable)
        {
            if (tower.Item1 == towerName)
            {
                if (playerMoney >= tower.Item2)
                {
                    Mediator.onEventFromManagers(new Tuple<string, object>("REMOVE_MONEY", tower.Item2));
                    return TowerFactory.createTower(tower.Item1);
                }

                Mediator.onEventFromManagers(new Tuple<string, object>("NO_MONEY", null));
                return null;
            }
        }

        throw new Exception("tower not found in towers available");
    }
}