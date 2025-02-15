using UnityEngine;

public abstract class A_EnemieAbstractFactory : MonoBehaviour
{
    protected T InstantiateEnemy<T>(GameObject prefab) where T : A_Enemie
    {
        GameObject instance = Instantiate(prefab);
        instance.SetActive(false);
        return instance.AddComponent<T>();
    }

    public abstract A_Enemie createWalkingEnemie();
    public abstract A_Enemie createFlyingEnemie();
    public abstract A_Enemie createTeleportingEnemie();
}