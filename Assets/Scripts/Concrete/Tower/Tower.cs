using System.Collections;
using UnityEngine;

public class Tower : A_Tower
{
    Transform rotor;
    GameObject projectile;

    private bool canShoot = true;

    private void Start()
    {
        Range = 20f;
        FireRate = 0.1f;
        ProjectileSpawn = findDeepChild(transform, "projectile").gameObject;
        rotor = findDeepChild(transform, "Turret");
        projectile = Resources.Load<GameObject>("Prefabs/projectile");
    }

    private void Update()
    {
        A_Enemie closestEnemy = GetClosestEnemyMethod(this, Range);
        trackClosestEnemy(closestEnemy);
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
            rotor.rotation = Quaternion.Slerp(rotor.rotation, targetRotation, Time.deltaTime * 25f);

            float angleDifference = Quaternion.Angle(rotor.rotation, targetRotation);
            // if (angleDifference < 5f)
            // {
                shoot();
            // }
        }
    }


    public override void setState(I_TowerState state)
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator Shoot()
    {
        canShoot = false;

        A_Enemie closestEnemy = GetClosestEnemyMethod(this, Range);

        if (closestEnemy != null && ProjectileSpawn != null)
        {
            GameObject newProjectile =
                Instantiate(projectile, ProjectileSpawn.transform.position, ProjectileSpawn.transform.rotation);

            newProjectile.transform.localScale *= 0.1f;

            Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
            rb.AddForce(newProjectile.transform.up * 50f, ForceMode.Impulse);

            Destroy(newProjectile, 2f);
        }

        yield return new WaitForSeconds(FireRate);

        canShoot = true;
    }

    public override void shoot()
    {
        if (canShoot)
        {
            StartCoroutine(Shoot());
        }
    }
}