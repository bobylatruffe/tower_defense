using System;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public GameObject direction;
    public NavMeshAgent agent;

    private void Update()
    {
        agent.SetDestination(direction.transform.position);
    }
}
