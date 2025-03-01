using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerFactory : MonoBehaviour, I_TowerFactory
{
    [SerializeField] private List<GameObject> towers;

    public A_Tower createTower(string towerName)
    {
        foreach (GameObject towerToCreate in towers)
        {
            if (towerToCreate.name == towerName)
            {
                GameObject tower = Instantiate(towerToCreate);
                tower.transform.localScale = Vector3.one * 0.1f;
                tower.SetActive(false);

                A_Tower atower = null;

                if (towerName == "Turret 1a" || towerName == "Turret 1b" || towerName == "Turret 1c" ||
                    towerName == "Turret 1d")
                {
                    atower = tower.AddComponent<TowerCanon>();
                    atower.possibleUpgrade = Resources.Load<PossibleUpgrade>("Turret1PossibleUpgrade");
                }

                if (towerName == "Turret 4a" || towerName == "Turret 4b" || towerName == "Turret 4c" ||
                    towerName == "Turret 4d")
                {
                    atower = tower.AddComponent<TowerFusee>();
                    atower.possibleUpgrade = Resources.Load<PossibleUpgrade>("Turret4PossibleUpgrade");
                }

                if (towerName == "Turret 6a" || towerName == "Turret 6b" || towerName == "Turret 6c" ||
                    towerName == "Turret 6d")
                {
                    atower = tower.AddComponent<TowerMitraillette>();
                    atower.possibleUpgrade = Resources.Load<PossibleUpgrade>("Turret6PossibleUpgrade");
                }

                if (towerName == "Turret 2a" || towerName == "Turret 2b" || towerName == "Turret 2c" ||
                    towerName == "Turret 2d")
                {
                    atower = tower.AddComponent<TowerMissile>();
                    tower.AddComponent<TrackClosestEnemyAndProjectileTrackEnemy>();
                    atower.possibleUpgrade = Resources.Load<PossibleUpgrade>("Turret2PossibleUpgrade");
                }

                return atower;
            }
        }

        throw new Exception("Invalid tower type");
    }
}
