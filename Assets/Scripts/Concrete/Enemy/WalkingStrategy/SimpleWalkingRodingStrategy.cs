using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

class SimpleWalkingRodingStrategy : MonoBehaviour, I_MoveStrategy
{
    private GameObject Destination { get; set; }
    private I_GameManagerMediator mediator;
    private NavMeshAgent agent;

    public void move()
    {
    }

    public void setDestination(GameObject destination)
    {
        Destination = destination;
    }

    public void initStrategy()
    {
        agent = gameObject.AddComponent<NavMeshAgent>();
        agent.autoBraking = true;

        float speed = 30f;
        agent.speed = speed;
        agent.acceleration = speed;
        agent.angularSpeed = 1000;
        agent.stoppingDistance = 2f;

        mediator = GameManager.Instance;


        StartCoroutine(changeDestination());
    }

    private IEnumerator changeDestination()
    {
        bool goToLeave = true;

        while (true)
        {
            string eventType = goToLeave ? "GET_LEAVE" : "GET_ENTRY";
            GameObject newDestination = mediator.onEventFromManagers(new Tuple<string, object>(eventType, null));

            if (newDestination != null)
            {
                agent.SetDestination(newDestination.transform.position);

                yield return new WaitUntil(() => !agent.pathPending);
                yield return new WaitUntil(() => agent.remainingDistance <= 2f);
            }
            else
            {
                Debug.LogWarning($"⚠ Aucune destination trouvée pour {eventType} !");
            }

            goToLeave = !goToLeave;
        }
    }
}