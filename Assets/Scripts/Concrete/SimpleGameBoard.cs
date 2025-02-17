using System;
using System.Linq;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class SimpleGameboard : A_GameboardManager
{
    private GameObject wall;
    private GameObject lastHitObject;
    private Color originalColor;
    private Vector3 lastMousePosition;


    public void Start()
    {
        wall = Resources.Load("Prefabs/Wall", typeof(GameObject)) as GameObject;

        Rows = 11;
        Cols = 30;

        Entries.AddRange(GameObject.FindGameObjectsWithTag("Spawn"));
        Leaves.AddRange(GameObject.FindGameObjectsWithTag("Finish"));
    }

    void Update()
    {
        whichCaseSelected();
    }

    private void whichCaseSelected()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        float maxDistance = Cols * 2;
        int groundLayer = LayerMask.GetMask("Ground");

        if (Physics.Raycast(ray, out hit, maxDistance, groundLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (lastHitObject != null && hitObject != lastHitObject)
            {
                lastHitObject.GetComponent<Renderer>().material.color = originalColor;
            }

            if (lastHitObject != hitObject)
            {
                originalColor = hitObject.GetComponent<Renderer>().material.color;
            }

            hitObject.GetComponent<Renderer>().material.color = Color.green;

            lastHitObject = hitObject;
        }
        else
        {
            if (lastHitObject != null)
            {
                lastHitObject.GetComponent<Renderer>().material.color = originalColor;
                lastHitObject = null;
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
    }


    // private bool checkIfPossibleToAddTower(Vector3 possibleNewTower)
    // {
    //     GameObject tempObstacle = Instantiate(wall, possibleNewTower + Vector3.up, Quaternion.identity);
    //     surface.BuildNavMesh();
    //
    //     foreach (A_Enemie enemy in Enemies)
    //     {
    //         if (enemy == null || enemy.gameObject == null) continue;
    //
    //         NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
    //         if (agent == null) continue;
    //
    //         Vector3 src = enemy.transform.position;
    //         Vector3 dst = agent.destination;
    //
    //         NavMeshPath path = new NavMeshPath();
    //         bool pathExists = NavMesh.CalculatePath(src, dst, NavMesh.AllAreas, path);
    //
    //         if (!pathExists || path.status != NavMeshPathStatus.PathComplete)
    //         {
    //             Destroy(tempObstacle);
    //             surface.BuildNavMesh();
    //             return false;
    //         }
    //     }
    //
    //     Destroy(tempObstacle);
    //     surface.BuildNavMesh();
    //     return true;
    // }

    public override void addEnemie(A_Enemie newEnemie)
    {
        newEnemie.transform.SetParent(transform);

        int whichEntry = Random.Range(0, Rows);

        newEnemie.transform.position = Entries[whichEntry].transform.position;
        newEnemie.transform.rotation = Entries[whichEntry].transform.rotation;
        newEnemie.transform.localScale = Entries[whichEntry].transform.localScale;

        newEnemie.gameObject.SetActive(true);
        newEnemie.IsMoving = true;
        newEnemie.GetComponent<I_MoveStrategy>().initStrategy();

        Enemies.Add(newEnemie);
    }

    public override void addTower(I_Tower newTower)
    {
        throw new NotImplementedException();
    }

    public override void upgradeTower(I_Tower towerToUpgrade)
    {
        throw new NotImplementedException();
    }

    public override GameObject getLeave()
    {
        return Leaves[Random.Range(0, Leaves.Count)];
    }

    public override GameObject getEntry()
    {
        return Entries[Random.Range(0, Leaves.Count)];
    }
}