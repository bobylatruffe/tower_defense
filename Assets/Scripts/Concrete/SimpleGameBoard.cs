using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleGameboard : A_GameboardManager
{
    private GameObject wall;
    private GameObject lastHitObject;
    private Color originalColor;
    private Vector3 lastMousePosition;
    private A_Tower pendingTower;
    private List<GameObject> lastAdjacentObjects = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Mediator = GameManager.Instance;
        wall = Resources.Load("Prefabs/Wall", typeof(GameObject)) as GameObject;

        Rows = 11;
        Cols = 30;

        Entries.AddRange(GameObject.FindGameObjectsWithTag("Spawn"));
        Leaves.AddRange(GameObject.FindGameObjectsWithTag("Finish"));
    }

    void Update()
    {
        if (pendingTower != null)
        {
            whichCaseSelected();
        }
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

            if (lastHitObject != null && lastHitObject != hitObject)
            {
                lastHitObject.GetComponent<Renderer>().material.color = originalColor;
            }

            foreach (GameObject adjacent in lastAdjacentObjects)
            {
                adjacent.GetComponent<Renderer>().material.color = originalColor;
            }

            lastAdjacentObjects.Clear();

            if (lastHitObject != hitObject)
            {
                originalColor = hitObject.GetComponent<Renderer>().material.color;
            }

            CheckAdjacentCubes(hitObject);

            if (lastAdjacentObjects.Count < 4)
            {
                foreach (GameObject adjacent in lastAdjacentObjects)
                {
                    adjacent.GetComponent<Renderer>().material.color = originalColor;
                }

                return;
            }

            hitObject.GetComponent<Renderer>().material.color = Color.green;
            lastHitObject = hitObject;

            pendingTower.gameObject.transform.position = hitObject.transform.position + Vector3.up * 0.5f;

            if (Input.GetMouseButtonDown(0))
            {
                pendingTower.gameObject.SetActive(true);
                pendingTower.gameObject.layer = LayerMask.NameToLayer("ItemAdded");
                pendingTower.transform.SetParent(transform);
                pendingTower = null;
                foreach (GameObject adjacentObject in lastAdjacentObjects)
                {
                    adjacentObject.layer = LayerMask.NameToLayer("Default");
                }

                hitObject.layer = LayerMask.NameToLayer("Default");

                lastHitObject.GetComponent<Renderer>().material.color = originalColor;
                foreach (GameObject adjacent in lastAdjacentObjects)
                {
                    adjacent.GetComponent<Renderer>().material.color = originalColor;
                }

                lastAdjacentObjects.Clear();
            }
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

    private void CheckAdjacentCubes(GameObject centerCube)
    {
        float detectionDistance = 1.1f;
        Vector3[] directions = { Vector3.right, Vector3.left, Vector3.forward, Vector3.back };

        foreach (Vector3 direction in directions)
        {
            RaycastHit hit;
            if (Physics.Raycast(centerCube.transform.position, direction, out hit, detectionDistance,
                    LayerMask.GetMask("Ground")))
            {
                GameObject adjacentCube = hit.collider.gameObject;
                adjacentCube.GetComponent<Renderer>().material.color = Color.green;
                lastAdjacentObjects.Add(adjacentCube);
            }
        }
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

        newEnemie.enemyTouchedByProjectile = enemyTouchedByProjectile;

        Enemies.Add(newEnemie);
    }

    public override void addTower(A_Tower tower)
    {
        pendingTower = tower;
    }

    public override void upgradeTower(A_Tower aTowerToUpgrade, A_Tower newTowerUpgraded)
    {
        if (newTowerUpgraded == null || aTowerToUpgrade == null) return;

        newTowerUpgraded.transform.SetParent(transform);
        newTowerUpgraded.transform.position = aTowerToUpgrade.transform.position;
        newTowerUpgraded.transform.rotation = aTowerToUpgrade.transform.rotation;
        newTowerUpgraded.gameObject.SetActive(true);
        newTowerUpgraded.gameObject.layer = LayerMask.NameToLayer("ItemAdded");

        Destroy(aTowerToUpgrade.gameObject);
    }

    public override GameObject getLeave()
    {
        return Leaves[Random.Range(0, Leaves.Count)];
    }

    public override GameObject getEntry()
    {
        return Entries[Random.Range(0, Leaves.Count)];
    }

    public override void enemyWin(GameObject enemyGo)
    {
        A_Enemie enemy = enemyGo.GetComponent<A_Enemie>();
        Mediator.onEventFromManagers(new Tuple<string, object>("REMOVE_LIFE", enemy.Point));
        Destroy(enemyGo);
        Enemies.Remove(enemy);
    }

    public void enemyTouchedByProjectile(GameObject enemyTouched, float projectileDammage)
    {
        A_Enemie enemy = enemyTouched.GetComponent<A_Enemie>();

        if (enemy.CurrentHealth - projectileDammage <= 0)
        {
            Enemies.Remove(enemyTouched.GetComponent<A_Enemie>());
            // Pour l'animation death
            // enemyTouched.GetComponent<BoxCollider>().enabled = false;
            // Destroy(enemyTouched, 2);
            Destroy(enemyTouched);
        }
        else
        {
            enemy.CurrentHealth -= projectileDammage;
        }
    }
}
