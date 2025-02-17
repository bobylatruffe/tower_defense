using System;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    private NavMeshAgent agent;

    private RaycastHit[] hits = new RaycastHit[1];

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.RaycastNonAlloc(ray, hits) > 0)
            {
                agent.destination = hits[0].point;
            }
        }
    }
}
