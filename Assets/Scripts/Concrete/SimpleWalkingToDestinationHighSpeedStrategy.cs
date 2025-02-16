using UnityEngine;
using UnityEngine.AI;

class SimpleWalkingToDestinationHighSpeedStrategy : MonoBehaviour, I_MoveStrategy
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
        agent.speed = 10f;
        agent.SetDestination(Destination.transform.position);
    }
}