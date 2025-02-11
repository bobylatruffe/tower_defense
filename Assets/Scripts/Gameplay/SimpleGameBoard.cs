using System;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleGameboard : A_GameboardManager
{
    [SerializeField] private GameObject cubePrefab;

    private GameObject lastHitObject;
    private Color originalColor;

    public void Start()
    {
        I_GameManagerMediator mediator = GameManager.Instance;
        Mediator = mediator;
        Rows = 10;
        Cols = 10;
        GenerateGrid();
    }

    void Update()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        float maxDistance = Cols * 2;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log("Raycast touche : " + hitObject.name);

            Renderer objRenderer = hitObject.GetComponent<Renderer>();
            if (objRenderer)
            {
                if (lastHitObject != null && lastHitObject != hitObject)
                {
                    lastHitObject.GetComponent<Renderer>().material.color = originalColor;
                }

                if (lastHitObject != hitObject)
                {
                    originalColor = objRenderer.material.color;
                }

                objRenderer.material.color = Color.red;
                lastHitObject = hitObject;
            }
        }
        else
        {
            if (lastHitObject)
            {
                lastHitObject.GetComponent<Renderer>().material.color = originalColor;
                lastHitObject = null;
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
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
