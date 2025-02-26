using System;
using System.Collections.Generic;
using UnityEngine;

public class Tower : A_Tower
{
    private void Start()
    {
        base.Start();
        Strategy = GetComponent<I_TowerStrategy>();
    }

    private void Update()
    {
        shoot();
    }

    public override void shoot()
    {
        List<A_Enemie> enemies = (List<A_Enemie>)Mediator.onEventFromManagers(
            new Tuple<EventTypeFromManager, object>(EventTypeFromManager.GET_ALL_ENEMIES, null));

        Strategy.shoot(enemies);
    }
}
