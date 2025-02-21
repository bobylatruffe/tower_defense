using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerFactory : MonoBehaviour, I_TowerFactory
{
    [SerializeField] List<GameObject> towers;

    public A_Tower createTower(string towerName)
    {
        foreach (GameObject towerToCreate in towers)
        {
            if (towerToCreate.name == towerName)
            {
                GameObject tower = Instantiate(towerToCreate);
                tower.transform.localScale = Vector3.one * 0.1f;
                tower.SetActive(false);

                if (towerName == "Turret 1a")
                    tower.AddComponent<TrackFirstClosestEnemy>();

                if (towerName == "Turret 4a")
                    tower.AddComponent<TrackFirstClosestEnemyMultipleProjectile>();

                if (towerName == "Turret 2a")
                    tower.AddComponent<TrackFirstClosestEnemyMultipleProjectileTrackEnemy>();

                return tower.AddComponent<Tower>();
            }
        }

        throw new Exception("Invalid tower type");
    }
}