using System;
using UnityEngine;

public class SimplePlayer : A_PlayerManager
{
    private void Start()
    {
        LifePoints = 100;
        Mediator.onEventFromManagers(new Tuple<string, object>("UPDATE_LIFE_POINT", null));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            showMenu();
        }
    }

    public override int removeLifePoint(int pointsToRemove)
    {
        LifePoints -= pointsToRemove;
        return LifePoints;
    }
}