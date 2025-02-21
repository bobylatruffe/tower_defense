using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileData projectileData;

    public void CopyFrom(ProjectileData data)
    {
        projectileData = data;
    }
}