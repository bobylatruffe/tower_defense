using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SimpleGameboard : A_GameboardManager
{
    private GameObject cubePrefab;
    private GameObject lastHitObject;
    private Color originalColor;

    public void Start()
    {
        cubePrefab = Resources.Load<GameObject>("Prefabs/Ground");
        I_GameManagerMediator mediator = GameManager.Instance;
        Mediator = mediator;
        Rows = 11;
        Cols = 20;
        EntryPoint = new Tuple<int, int>(0, -1);
        LeavePoint = new Tuple<int, int>(0, Cols);
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

        GameObject entryPoint = Instantiate(cubePrefab, new Vector3(EntryPoint.Item1, 1, EntryPoint.Item2),
            Quaternion.identity, transform);
        GameObject leavePoint = Instantiate(cubePrefab, new Vector3(LeavePoint.Item1, 1, LeavePoint.Item2),
            Quaternion.identity, transform);

        entryPoint.GetComponent<Renderer>().material.color = Color.magenta;
        leavePoint.GetComponent<Renderer>().material.color = Color.blue;
    }

    public override void addEnemie(A_Enemie newEnemie)
    {
        throw new NotImplementedException();
    }

    public override void addTower(I_Tower newTower)
    {
        throw new NotImplementedException();
    }

    public override void upgradeTower(I_Tower towerToUpgrade)
    {
        throw new NotImplementedException();
    }
}