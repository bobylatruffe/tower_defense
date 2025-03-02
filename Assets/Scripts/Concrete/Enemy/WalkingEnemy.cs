using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WalkingEnemy : A_Enemy
{
    private Animator animator;
    private NavMeshAgent agent;
    private ProgressBarPro progressBar;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        Mediator = GameManager.Instance;
        MaxHealth = 5;
        CurrentHealth = MaxHealth;
        Point = 10;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        EnemyType = EnemyType.WALKING;
        progressBar = transform.Find("progressBar").gameObject.GetComponent<ProgressBarPro>();
    }

    private void Update()
    {
        if (IsMoving)
        {
            MoveStrategy.move();
            animator.SetFloat("Speed", agent.velocity.magnitude);
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
            agent.speed = 0f;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            animator.SetTrigger("Death");

            StartCoroutine(sendDeathEvent());
        }
    }

    private IEnumerator sendDeathEvent()
    {
        Mediator.onEventFromManagers(
            new Tuple<EventTypeFromManager, object>(EventTypeFromManager.ENEMY_IS_DEATH, gameObject));

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
