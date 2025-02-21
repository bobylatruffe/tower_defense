using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Scriptable Objects/ProjectileData")]
public class ProjectileData : ScriptableObject
{
    public GameObject projectilePrefab;
    public int projectileDamage;
    public float projectileSpeed;
    public float projectileRange;
    public float projectileFireRate;
}
