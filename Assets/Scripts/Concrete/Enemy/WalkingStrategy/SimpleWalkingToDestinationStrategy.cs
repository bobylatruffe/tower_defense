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
        float speed = Random.Range(2f, 8f);
        agent.speed = speed;
        agent.acceleration = speed;
        agent.angularSpeed = 360f;
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

        agent.SetDestination(Destination.transform.position);
    }
}
