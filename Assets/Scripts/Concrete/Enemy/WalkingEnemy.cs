using System;
using UnityEngine;
using UnityEngine.AI;

public class WalkingEnemy : A_Enemie
{
    private Animator animator;
    private NavMeshAgent agent;

    private void Start()
    {
        Point = 10;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (IsMoving)
        {
            MoveStrategy.move();
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        agent.enabled = false;
        animator.SetTrigger("Death");
        enemyTouchedByProjectile(gameObject);
    }
}