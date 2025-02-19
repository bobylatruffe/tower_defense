using System.Collections.Generic;
using UnityEngine;

public class SimpleAEnemieFactory : MonoBehaviour, I_EnemieAbstractFactory
{
    [SerializeField] private List<GameObject> prefabEnemiesWalking = new List<GameObject>();
    [SerializeField] private List<GameObject> prefabEnemiesFlying = new List<GameObject>();
    [SerializeField] private List<GameObject> prefabEnemiesTeleporting = new List<GameObject>();


    protected T InstantiateEnemy<T>(GameObject prefab) where T : A_Enemie
    {
        GameObject instance = Instantiate(prefab);
        instance.SetActive(false);
        return instance.AddComponent<T>();
    }

    public A_Enemie createWalkingEnemie()
    {
        return InstantiateEnemy<WalkingEnemy>(prefabEnemiesWalking[Random.Range(0, prefabEnemiesWalking.Count)]);
    }

    public A_Enemie createFlyingEnemie()
    {
        return InstantiateEnemy<FlyingEnemy>(prefabEnemiesFlying[Random.Range(0, prefabEnemiesFlying.Count)]);
    }

    public A_Enemie createTeleportingEnemie()
    {
        return InstantiateEnemy<TeleportingEnemy>(
            prefabEnemiesTeleporting[Random.Range(0, prefabEnemiesTeleporting.Count)]);
    }
}