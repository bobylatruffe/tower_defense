using System;
using UnityEngine;

public class DetectorEnemyWin : MonoBehaviour
{
    private I_DetectorEnemyWin GameboardObserver { get; set; }

    private void Start()
    {
        GameboardObserver = A_Gameboard.Instance;
    }

    private void Update()
    {
        Collider[] colliders =
            Physics.OverlapBox(transform.position + Vector3.up * 10.51f, new Vector3(25f, 10, 0.495f),
                Quaternion.identity, LayerMask.GetMask("Enemy"));

        foreach (Collider col in colliders)
        {
            if (col.gameObject)
                GameboardObserver.enemyWin(col.gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.up * 10.51f,
            new Vector3(50, 20, 0.99f));
    }
}
