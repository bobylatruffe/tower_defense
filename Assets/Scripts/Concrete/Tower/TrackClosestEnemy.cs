using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrackClosestEnemy : MonoBehaviour, I_TowerStrategy
{
    private Transform rotor;
    private ProjectileData projectileData;
    private GameObject projectileSpawn;
    private List<GameObject> projectiles = new List<GameObject>();

    private float range;
    private float fireRate;
    private bool canShoot = true;

    private A_Enemie currentTarget;

    private void Start()
    {
        projectileSpawn = findDeepChild(transform, "projectilesSpawn").gameObject;
        rotor = findDeepChild(transform, "Turret");
        projectileData = projectileSpawn.GetComponent<Projectile>().projectileData;
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

    private void trackTarget()
    {
        if (currentTarget == null)
            return;

        Vector3 futurePosition = PredictFuturePosition(currentTarget, projectileData.projectileSpeed);
        Vector3 direction = futurePosition - rotor.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        targetRotation *= Quaternion.Euler(0, -90, 0);
        rotor.rotation = Quaternion.Slerp(rotor.rotation, targetRotation, Time.deltaTime * 50f);

        float angleDifference = Quaternion.Angle(rotor.rotation, targetRotation);
        if (angleDifference < 5f && canShoot)
        {
            StartCoroutine(Shoot(currentTarget));
        }
    }

    private A_Enemie getClosestEnemy(List<A_Enemie> enemies, float range)
    {
        A_Enemie closestEnemy = null;
        float closestDistance = float.MaxValue;
        Vector3 towerPosition = gameObject.transform.position;

        foreach (A_Enemie enemy in enemies)
        {
            if (enemy == null) continue;

            float distance = Vector3.Distance(towerPosition, enemy.transform.position);

            if (distance < closestDistance && distance <= range)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    private Vector3 PredictFuturePosition(A_Enemie enemy, float projectileSpeed)
    {
        if (enemy == null) return enemy.transform.position;

        NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
        if (agent == null) return enemy.transform.position;

        Vector3 enemyPosition = enemy.transform.position;
        Vector3 enemyVelocity = agent.velocity;

        float timeToTarget = Vector3.Distance(transform.position, enemyPosition) / projectileSpeed;

        return enemyPosition + enemyVelocity * timeToTarget;
    }


    private IEnumerator Shoot(A_Enemie target)
    {
        canShoot = false;

        if (target != null && projectileSpawn != null)
        {
            foreach (GameObject projectile in projectiles)
            {
                GameObject newProjectile =
                    Instantiate(projectileData.projectilePrefab,
                        projectile.transform.position,
                        projectile.transform.rotation);

                newProjectile.AddComponent<Projectile>().CopyFrom(projectileData);

                Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
                rb.AddForce(newProjectile.transform.up * projectileData.projectileSpeed, ForceMode.Impulse);

                Destroy(newProjectile, 5f);
            }
        }

        yield return new WaitForSeconds(fireRate);

        canShoot = true;
    }

    public void shoot(List<A_Enemie> enemies)
    {
        if (currentTarget == null || Vector3.Distance(transform.position, currentTarget.transform.position) > range)
        {
            currentTarget = getClosestEnemy(enemies, range);
        }

        if (currentTarget != null)
        {
            trackTarget();
        }
    }
}
