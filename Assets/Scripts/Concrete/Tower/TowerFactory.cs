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

                A_Tower atower = tower.AddComponent<Tower>();

                if (towerName == "Turret 1a" || towerName == "Turret 1b" || towerName == "Turret 1c" ||
                    towerName == "Turret 1d")
                {
                    tower.AddComponent<TrackFirstClosestEnemyMultipleProjectile>();
                    atower.possibleUpgrade = Resources.Load<PossibleUpgrade>("Turret1PossibleUpgrade");
                }

                if (towerName == "Turret 4a" || towerName == "Turret 4b" || towerName == "Turret 4c" ||
                    towerName == "Turret 4d")
                {
                    tower.AddComponent<TrackFirstClosestEnemyMultipleProjectile>();
                    atower.possibleUpgrade = Resources.Load<PossibleUpgrade>("Turret4PossibleUpgrade");
                }

                if (towerName == "Turret 2a" || towerName == "Turret 2b" || towerName == "Turret 2c" ||
                    towerName == "Turret 2d")
                {
                    tower.AddComponent<TrackFirstClosestEnemyMultipleProjectileTrackEnemy>();
                    atower.possibleUpgrade = Resources.Load<PossibleUpgrade>("Turret2PossibleUpgrade");
                }

                return atower;
            }
        }

        throw new Exception("Invalid tower type");
    }
}