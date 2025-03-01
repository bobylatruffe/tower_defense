using System;
using System.Collections.Generic;
using Michsky.MUIP;
using UnityEngine;

public class TowerMissile : A_Tower
{
    private HorizontalSelector strategySelector;

    private void Start()
    {
        base.Start();
        Strategy = GetComponent<I_TowerStrategy>();

        TowerOptions = Resources.Load<GameObject>("Prefabs/towerOptions");
        TowerOptions = Instantiate(TowerOptions, transform);
        TowerOptions.SetActive(false);

        strategySelector = findDeepChild(transform, "strategySelector").GetComponent<HorizontalSelector>();
        strategySelector.gameObject.SetActive(false);
        // strategySelector.onValueChanged.AddListener(onStrategiesChanged);
        // onStrategiesChanged(strategySelector.index);
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

    private void onStrategiesChanged(int index)
    {

        switch (index)
        {
            case 0:
                Strategy = gameObject.AddComponent<TrackClosestEnemyAndTrack>();
                break;
            case 1:
                Strategy = gameObject.AddComponent<TrackFarthestEnemyAndTrack>();
                break;
            case 2:
                Strategy = gameObject.AddComponent<TrackOnlyClosestEnemy>();
                break;
            case 3:
                Strategy = gameObject.AddComponent<TrackClosestEnemyAndProjectileTrackEnemy>();
                break;
        }
    }

    private Transform findDeepChild(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
                return child;

            Transform found = findDeepChild(child, childName);
            if (found != null)
                return found;
        }

        return null;
    }
}
