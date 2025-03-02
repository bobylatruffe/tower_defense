using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class FlyingEnemy : A_Enemy
{
    private ProgressBarPro progressBar;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        Mediator = GameManager.Instance;
        MaxHealth = 5;
        CurrentHealth = MaxHealth;
        Point = 10;
        EnemyType = EnemyType.FLYING;
        progressBar = transform.Find("progressBar").gameObject.GetComponent<ProgressBarPro>();
    }

    private void Update()
    {
        if (IsMoving)
        {
            MoveStrategy.move();
        }

        progressBar.transform.LookAt(progressBar.transform.position + cam.transform.forward);
    }

    private void OnTriggerEnter(Collider projectile)
    {
        ProjectileData projectileData = projectile.GetComponent<Projectile>().projectileData;
        Destroy(projectile.gameObject);

        CurrentHealth -= projectileData.projectileDamage;

        if (progressBar)
        {
            progressBar.SetValue(CurrentHealth, MaxHealth);
        }

        if (CurrentHealth <= 0)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Mediator.onEventFromManagers(
                new Tuple<EventTypeFromManager, object>(EventTypeFromManager.ENEMY_IS_DEATH, gameObject));
            Destroy(gameObject);
        }
    }
}
