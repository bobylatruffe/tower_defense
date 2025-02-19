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
}