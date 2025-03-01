using System;
using System.Collections.Generic;
using Michsky.MUIP;
using UnityEngine;

public class TowerMitraillette : A_Tower
{
    private HorizontalSelector strategySelector;

    private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        shoot();
    }

    private void OnMouseDown()
    {
        TowerOptions.SetActive(!TowerOptions.activeSelf);
    }

    public override void shoot()
    {
        List<A_Enemy> enemies = (List<A_Enemy>)Mediator.onEventFromManagers(
            new Tuple<EventTypeFromManager, object>(EventTypeFromManager.GET_ALL_ENEMIES, null));

        Strategy.shoot(enemies);
    }
}
