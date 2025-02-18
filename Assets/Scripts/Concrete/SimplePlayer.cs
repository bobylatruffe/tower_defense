using System;
using UnityEngine;

public class SimplePlayer : A_PlayerManager
{
    private void Start()
    {
        LifePoints = 100;
        Money = 100;
        Mediator.onEventFromManagers(new Tuple<string, object>("UPDATE_LIFE_POINT", LifePoints));
        Mediator.onEventFromManagers(new Tuple<string, object>("UPDATE_MONEY", Money));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            showMenu();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            showTowerShop();
        }
    }

    public override int removeLifePoint(int pointsToRemove)
    {
        LifePoints -= pointsToRemove;
        return LifePoints;
    }
}