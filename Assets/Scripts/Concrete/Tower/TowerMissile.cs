using System;
using System.Collections.Generic;
using System.Linq;
using Michsky.MUIP;
using UnityEngine;

public class TowerMissile : A_Tower
{
    private HorizontalSelector strategySelector;

    private void Start()
    {
        base.Start();
        StrategySelector.gameObject.SetActive(false);
    }

    private void Update()
    {
        shoot();
    }

    public override void shoot()
    {
        if (Strategy == null) return;

        List<A_Enemy> enemies = ((List<A_Enemy>)Mediator.onEventFromManagers(
                new Tuple<EventTypeFromManager, object>(EventTypeFromManager.GET_ALL_ENEMIES, null)))
            .Where(enemy => enemy.EnemyType == EnemyType.FLYING)
            .ToList();

        Strategy.shoot(enemies);
    }
}
