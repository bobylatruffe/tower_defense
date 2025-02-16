using System;
using System.Linq;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class SimpleGameboard : A_GameboardManager
{
    private GameObject cubePrefab;
    private GameObject lastHitObject;
    private Color originalColor;

    private NavMeshSurface navMeshSurface;

    public void Start()
    {
        cubePrefab = Resources.Load<GameObject>("Prefabs/Ground");
        Rows = 11;
        Cols = 30;
        GenerateGrid();
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

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            handleSelectedCase(hit.collider.gameObject);
        }
        else
        {
            resetLastCase();
        }

        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
    }

    private void handleSelectedCase(GameObject hitObject)
    {
        if (hitObject == lastHitObject) return;

        resetLastCase();

        Renderer objRenderer = hitObject.GetComponent<Renderer>();
        if (objRenderer)
        {
            originalColor = objRenderer.material.color;
            objRenderer.material.color = Color.red;
            lastHitObject = hitObject;
        }
    }

    private void resetLastCase()
    {
        if (lastHitObject)
        {
            Renderer lastRenderer = lastHitObject.GetComponent<Renderer>();
            if (lastRenderer)
            {
                lastRenderer.material.color = originalColor;
            }

            lastHitObject = null;
        }
    }

    private void GenerateGrid()
    {
        float offsetX = (Rows - 1) / 2.0f;
        // float offsetZ = (gameboardLogic.Cols - 1) / 2.0f;

        for (int x = 0; x < Rows; x++)
        {
            for (int z = 0; z < Cols; z++)
            {
                Vector3 position = new Vector3(x - offsetX, 0, z);
                Instantiate(cubePrefab, position, Quaternion.identity, transform);
            }
        }

        for (int x = 0; x < Rows; x++)
        {
            Entries.Add(Instantiate(cubePrefab, new Vector3(x - offsetX, 0f, -1), Quaternion.identity, transform));
            Entries[x].GetComponent<Renderer>().material.color = Color.magenta;

            Leaves.Add(Instantiate(cubePrefab, new Vector3(x - offsetX, 0f, Cols), Quaternion.identity, transform));
            Leaves[x].GetComponent<Renderer>().material.color = Color.black;
            Leaves[x].AddComponent<BoxCollider>();
            Leaves[x].GetComponent<BoxCollider>().isTrigger = true;
        }


        navMeshSurface = gameObject.AddComponent<NavMeshSurface>();
        navMeshSurface.collectObjects = CollectObjects.Children;
        navMeshSurface.BuildNavMesh();
    }

    public override void addEnemie(A_Enemie newEnemie)
    {
        newEnemie.transform.SetParent(transform);

        int whichEntry = Random.Range(0, Rows);
        int whichLeave = Random.Range(0, Rows);

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