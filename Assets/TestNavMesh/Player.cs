using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface surface;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Transform target;

    void Update()
    {
        // surface.BuildNavMesh();
        agent.SetDestination(target.position);
    }
}
