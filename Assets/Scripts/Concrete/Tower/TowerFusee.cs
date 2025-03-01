using System;
using System.Collections.Generic;
using Michsky.MUIP;
using UnityEngine;

public class TowerFusee : A_Tower
{

    private void Start()
    {
        base.Start();
        StrategySelector.onValueChanged.AddListener(onStrategiesChanged);
        onStrategiesChanged(StrategySelector.index);
    }

    private void Update()
    {
        shoot();
    }

    public override void shoot()
    {
        if (Strategy == null) return;

        List<A_Enemy> enemies = (List<A_Enemy>)Mediator.onEventFromManagers(
            new Tuple<EventTypeFromManager, object>(EventTypeFromManager.GET_ALL_ENEMIES, null));

        Strategy.shoot(enemies);
    }
}
