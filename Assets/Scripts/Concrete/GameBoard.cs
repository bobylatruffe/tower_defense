using System;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameBoard : A_Gameboard
{
    private GameObject wall;
    private GameObject lastHitObject;
    private Color originalColor;
    private Vector3 lastMousePosition;
    private A_Tower pendingTower;

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

            pendingTower.gameObject.transform.position = hitObject.transform.position + Vector3.up * 0.5f;

            if (Input.GetMouseButtonDown(0))
            {
                pendingTower.gameObject.SetActive(true);
                pendingTower.gameObject.layer = LayerMask.NameToLayer("ItemAdded");
                pendingTower.transform.SetParent(transform);
                // pendingTower.GetEnemies = () => Enemies;
                Mediator.onEventFromManagers(
                    new Tuple<EventTypeFromManager, object>(EventTypeFromManager.REMOVE_MONEY, pendingTower.Cost));
                pendingTower = null;
                hitObject.layer = LayerMask.NameToLayer("Default");
                lastHitObject.GetComponent<Renderer>().material.color = originalColor;
                gameObject.GetComponent<NavMeshSurface>().BuildNavMesh();
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

    public override bool addEnemie(A_Enemy newEnemy)
    {
        newEnemy.transform.SetParent(transform);

        int whichEntry = Random.Range(0, Rows);

        newEnemy.transform.position = Entries[whichEntry].transform.position;
        newEnemy.transform.rotation = Entries[whichEntry].transform.rotation;
        newEnemy.transform.localScale = Entries[whichEntry].transform.localScale;

        newEnemy.gameObject.SetActive(true);
        newEnemy.IsMoving = true;
        newEnemy.GetComponent<I_MoveStrategy>().initStrategy();

        Enemies.Add(newEnemy);

        return true;
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
        Mediator.onEventFromManagers(
            new Tuple<EventTypeFromManager, object>(EventTypeFromManager.REMOVE_MONEY, newTowerUpgraded.Cost));

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
        A_Enemy enemy = enemyGo.GetComponent<A_Enemy>();
        Mediator.onEventFromManagers(
            new Tuple<EventTypeFromManager, object>(EventTypeFromManager.REMOVE_LIFE, enemy.Point));
        Destroy(enemyGo);
        Enemies.Remove(enemy);
    }

    public override void enemyIsDeath(GameObject enemyDeath)
    {
        Enemies.Remove(enemyDeath.GetComponent<A_Enemy>());
        Destroy(enemyDeath);
    }
}
