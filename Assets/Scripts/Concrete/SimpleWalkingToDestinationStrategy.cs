using UnityEngine;
using UnityEngine.AI;

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
        agent.SetDestination(Destination.transform.position);
    }
}