using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackClosestEnemyAndProjectileTrackEnemy : MonoBehaviour, I_TowerStrategy
{
    private Transform rotor;
    private ProjectileData projectileData;
    private GameObject projectileSpawn;
    private List<GameObject> projectiles = new List<GameObject>();

    private float range;
    private float fireRate;
    private bool canShoot = true;

    private List<A_Enemy> currentTargets = new List<A_Enemy>();

    private void Start()
    {
        projectileSpawn = gameObject.GetComponent<A_Tower>().ProjectileSpawn;
        rotor = findDeepChild(transform, "Turret");
        projectileData = gameObject.GetComponent<A_Tower>().ProjectileData;
        range = projectileData.projectileRange;
        fireRate = projectileData.projectileFireRate;
        foreach (Transform child in projectileSpawn.transform)
        {
            projectiles.Add(child.gameObject);
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

    private Vector3 getCenterOfTargets(List<A_Enemy> targets)
    {
        if (targets == null || targets.Count == 0)
            return Vector3.zero;

        Vector3 center = Vector3.zero;
        foreach (var target in targets)
        {
            center += target.transform.position;
        }

        return center / targets.Count;
    }

    private void trackTargets()
    {
        if (currentTargets.Count == 0)
            return;

        Vector3 centerPosition = getCenterOfTargets(currentTargets);
        Vector3 direction = centerPosition - rotor.position;
        direction.y = 0; // On ignore la hauteur

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        targetRotation *= Quaternion.Euler(0, -90, 0);
        rotor.rotation = Quaternion.Slerp(rotor.rotation, targetRotation, Time.deltaTime * 2f);

        // if (Quaternion.Angle(rotor.rotation, targetRotation) < 5f && canShoot)
        if (canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    private List<A_Enemy> getClosestEnemies(List<A_Enemy> enemies, float range, int maxTargets)
    {
        if (this == null) return null;
        return enemies
            .Where(e => e != null && Vector3.Distance(transform.position, e.transform.position) <= range)
            .OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
            .Take(maxTargets)
            .ToList();
    }

    private IEnumerator Shoot()
    {
        canShoot = false;

        if (currentTargets.Count > 0 && projectileSpawn != null)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                GameObject newProjectile = Instantiate(
                    projectileData.projectilePrefab,
                    projectiles[i].transform.position,
                    projectileSpawn.transform.rotation
                );

                Projectile projectileComponent = newProjectile.AddComponent<Projectile>();
                projectileComponent.CopyFrom(projectileData);

                TrackerProjectile trackerProjectile = newProjectile.AddComponent<TrackerProjectile>();
                trackerProjectile.SetTarget(currentTargets[i % currentTargets.Count]);

                Destroy(newProjectile, 5f);
            }
        }

        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    public void shoot(List<A_Enemy> enemies)
    {
        currentTargets = getClosestEnemies(enemies, range, projectiles.Count);

        if (currentTargets != null && currentTargets.Count > 0)
        {
            trackTargets();
        }
    }
}
