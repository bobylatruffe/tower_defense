using System;
using UnityEngine;

public class DetectorEnemyWin : MonoBehaviour
{
    private void Update()
    {
        Collider[] colliders =
            Physics.OverlapBox(transform.position + Vector3.up * 10.51f, new Vector3(5.5f, 10, 0.495f));

        foreach (Collider col in colliders)
        {
            Destroy(col.gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.up * 10.51f,
            new Vector3(11, 20, 0.99f));
    }
}