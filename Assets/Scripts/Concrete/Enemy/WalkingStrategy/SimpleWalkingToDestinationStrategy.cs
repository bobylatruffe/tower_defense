using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

class SimpleWalkingToDestinationStrategy : MonoBehaviour, I_MoveStrategy
{
    private GameObject Destination { get; set; }
    public void move()
    {
    }

    public void setDestination(GameObject destination)
    {
        Destination = destination;
    }

    public void initStrategy()
    {
        NavMeshAgent agent = gameObject.AddComponent<NavMeshAgent>();
        agent.autoBraking = false;
        // agent.speed = Random.Range(0.5f, 1f);
        float speed = Random.Range(5f, 10f);
        agent.speed = speed;
        agent.acceleration = speed;
        agent.angularSpeed = 1000f;

        agent.SetDestination(Destination.transform.position);
    }
}