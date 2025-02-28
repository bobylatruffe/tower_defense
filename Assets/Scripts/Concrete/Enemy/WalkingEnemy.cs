using System;
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

        progressBar = transform.Find("progressBar").gameObject.GetComponent<ProgressBarPro>();
    }

    private void Update()
    {
        if (IsMoving)
        {
            MoveStrategy.move();
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }

        if (progressBar)
            progressBar.transform.LookAt(progressBar.transform.position + cam.transform.forward);
    }

    private void OnTriggerEnter(Collider projectile)
    {
        ProjectileData projectileData = projectile.GetComponent<Projectile>().projectileData;
        Destroy(projectile.gameObject);

        CurrentHealth -= projectileData.projectileDamage;
        // CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if (progressBar)
        {
            progressBar.SetValue(CurrentHealth, MaxHealth);
        }

        if (CurrentHealth <= 0)
            Mediator.onEventFromManagers(
                new Tuple<EventTypeFromManager, object>(EventTypeFromManager.ENEMY_IS_DEATH, gameObject));
    }
}
