using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStrategy : MonoBehaviour, I_TowerStrategy
{
    private Transform rotor;
    private GameObject projectile;
    private GameObject projectileSpawn;

    private float range = 2f;
    private float fireRate = 0.5f;
    private bool canShoot = true;
    private float speedProjectile = 100f;

    private void Start()
    {
        projectileSpawn = findDeepChild(transform, "projectile").gameObject;
        rotor = findDeepChild(transform, "Turret");
        projectile = Resources.Load<GameObject>("Prefabs/projectile");
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

    private void trackClosestEnemy(A_Enemie closestEnemy)
    {
        if (closestEnemy != null)
        {
            Vector3 direction = closestEnemy.transform.position - rotor.position;
            direction.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation *= Quaternion.Euler(0, -90, 0);
            rotor.rotation = Quaternion.Slerp(rotor.rotation, targetRotation, Time.deltaTime * 50f);

            float angleDifference = Quaternion.Angle(rotor.rotation, targetRotation);
            if (angleDifference < 5f && canShoot)
            {
                StartCoroutine(Shoot(closestEnemy));
            }
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

    private IEnumerator Shoot(A_Enemie closestEnemy)
    {
        canShoot = false;

        if (closestEnemy != null && projectileSpawn != null)
        {
            GameObject newProjectile =
                Instantiate(projectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);

            newProjectile.transform.localScale *= 0.3f;

            Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
            rb.AddForce(newProjectile.transform.up * speedProjectile, ForceMode.Impulse);

            Destroy(newProjectile, 1f);
        }

        yield return new WaitForSeconds(fireRate);

        canShoot = true;
    }


    public void shoot(List<A_Enemie> enemies)
    {
        A_Enemie closestEnemy = getClosestEnemy(enemies, range);
        if (closestEnemy)
        {
            trackClosestEnemy(closestEnemy);
        }
    }
}