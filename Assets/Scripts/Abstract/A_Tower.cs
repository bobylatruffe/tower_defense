using System;
using System.Collections.Generic;
using Michsky.MUIP;
using UnityEngine;

public abstract class A_Tower : MonoBehaviour
{
    protected I_GameManagerMediator Mediator { get; set; }

    public PossibleUpgrade possibleUpgrade { get; set; }

    protected GameObject TowerOptions { get; set; }

    protected I_TowerStrategy Strategy { get; set; }

    public int Cost { get; set; }

    public ProjectileData ProjectileData { get; set; }
    public GameObject ProjectileSpawn { get; set; }

    protected HorizontalSelector StrategySelector;

    public abstract void shoot();

    protected virtual void Start()
    {
        Mediator = GameManager.Instance;
        Strategy = GetComponent<I_TowerStrategy>();
        ProjectileSpawn = findDeepChild(transform, "projectilesSpawn").gameObject;
        ProjectileData = ProjectileSpawn.GetComponent<Projectile>().projectileData;

        TowerOptions = Resources.Load<GameObject>("Prefabs/towerOptions");
        TowerOptions = Instantiate(TowerOptions, transform);
        TowerOptions.SetActive(false);

        StrategySelector = findDeepChild(transform, "strategySelector").GetComponent<HorizontalSelector>();
    }

    protected void onStrategiesChanged(int index)
    {
        if (Strategy != null)
        {
            Destroy(Strategy as Component);
        }

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

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(1))
        {
            TowerOptions.SetActive(true);
        }
    }

    public Transform findDeepChild(Transform parent, string childName)
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
