using System;
using System.Collections.Generic;
using Michsky.MUIP;
using UnityEngine;

public class TowerMissile : A_Tower
{
    private HorizontalSelector strategySelector;

    private void Start()
    {
        Mediator = GameManager.Instance;
        Strategy = GetComponent<I_TowerStrategy>();
        TowerOptions = Resources.Load<GameObject>("Prefabs/towerOptions");
        TowerOptions = Instantiate(TowerOptions, transform);
        TowerOptions.SetActive(false);
        StrategySelector = findDeepChild(transform, "strategySelector").GetComponent<HorizontalSelector>();
        StrategySelector.gameObject.SetActive(false);
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
