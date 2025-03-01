using System;
using System.Collections.Generic;
using UnityEngine;

public class Shop : A_Shop
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

        towersAvailable.Add(new Tuple<string, int>("Turret 1a", 60));
        towersAvailable.Add(new Tuple<string, int>("Turret 1b", 120));
        towersAvailable.Add(new Tuple<string, int>("Turret 1c", 240));
        towersAvailable.Add(new Tuple<string, int>("Turret 1d", 480));

        towersAvailable.Add(new Tuple<string, int>("Turret 2a", 80));
        towersAvailable.Add(new Tuple<string, int>("Turret 2b", 160));
        towersAvailable.Add(new Tuple<string, int>("Turret 2c", 320));
        towersAvailable.Add(new Tuple<string, int>("Turret 2d", 640));

        towersAvailable.Add(new Tuple<string, int>("Turret 4a", 80));
        towersAvailable.Add(new Tuple<string, int>("Turret 4c", 160));
        towersAvailable.Add(new Tuple<string, int>("Turret 4d", 160));

        towersAvailable.Add(new Tuple<string, int>("Turret 5a", 100));

        towersAvailable.Add(new Tuple<string, int>("Turret 6a", 120));
        towersAvailable.Add(new Tuple<string, int>("Turret 6c", 220));
        towersAvailable.Add(new Tuple<string, int>("Turret 6d", 440));

        towersAvailable.Add(new Tuple<string, int>("Turret 7a", 140));

        towersAvailable.Add(new Tuple<string, int>("Turret 8a", 160));
    }

    // private void Update()
    // {
    //     Camera Cam = Camera.main;
    //     Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
    //
    //     RaycastHit hit;
    //     float maxDistance = 50;
    //     int groundLayer = LayerMask.GetMask("ItemAdded");
    //
    //     if (Physics.Raycast(ray, out hit, maxDistance, groundLayer))
    //     {
    //         if (Input.GetKeyDown(KeyCode.U))
    //         {
    //             A_Tower atower = hit.collider.gameObject.GetComponent<A_Tower>();
    //             string atowerName = atower.name.Replace("(Clone)", "");
    //             GameObject nextUpgradedTower = null;
    //             for (int i = 0; i < atower.possibleUpgrade.towers.Count; i++)
    //             {
    //                 if (atower.possibleUpgrade.towers[i].name == atowerName)
    //                 {
    //                     if (i + 1 < atower.possibleUpgrade.towers.Count)
    //                     {
    //                         nextUpgradedTower = atower.possibleUpgrade.towers[i + 1];
    //                     }
    //                     else
    //                     {
    //                         Debug.Log("No more upgrades available");
    //                     }
    //                 }
    //             }
    //
    //             if (nextUpgradedTower != null)
    //             {
    //                 A_Tower newATower = buyIfPlayerCanAffordIt(A_Player.Instance.Money, nextUpgradedTower.name);
    //                 A_Gameboard.Instance.upgradeTower(atower, newATower);
    //             }
    //         }
    //
    //         // if (Input.GetKeyDown(KeyCode.T))
    //         // {
    //         //     Debug.Log("Decorateur");
    //         //     GameObject towerGo = hit.collider.gameObject;
    //         //     towerGo.AddComponent<TowerDecorateur>();
    //         // }
    //     }
    //
    //     Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
    // }

    public override A_Tower buyIfPlayerCanAffordIt(int playerMoney, string towerName)
    {
        foreach (Tuple<string, int> tower in towersAvailable)
        {
            if (tower.Item1 == towerName)
            {
                if (playerMoney >= tower.Item2)
                {
                    A_Tower newtower = TowerFactory.createTower(tower.Item1);
                    newtower.Cost = tower.Item2;
                    return newtower;
                }

                Mediator.onEventFromManagers(
                    new Tuple<EventTypeFromManager, object>(EventTypeFromManager.NO_MONEY, null));
                return null;
            }
        }

        throw new Exception("tower not found in towers available");
    }
}
