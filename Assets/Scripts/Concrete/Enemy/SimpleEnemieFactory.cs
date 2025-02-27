using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemieFactory : MonoBehaviour, I_EnemieAbstractFactory
{
    [SerializeField] private List<GameObject> prefabEnemiesWalking = new List<GameObject>();
    [SerializeField] private List<GameObject> prefabEnemiesFlying = new List<GameObject>();
    [SerializeField] private List<GameObject> prefabEnemiesTeleporting = new List<GameObject>();


    protected T InstantiateEnemy<T>(GameObject prefab) where T : A_Enemy
    {
        GameObject instance = Instantiate(prefab);
        instance.SetActive(false);
        return instance.AddComponent<T>();
    }

    public A_Enemy createWalkingEnemie()
    {
        return InstantiateEnemy<WalkingEnemy>(prefabEnemiesWalking[Random.Range(0, prefabEnemiesWalking.Count)]);
    }

    public A_Enemy createFlyingEnemie()
    {
        return InstantiateEnemy<FlyingEnemy>(prefabEnemiesFlying[Random.Range(0, prefabEnemiesFlying.Count)]);
    }

    public A_Enemy createTeleportingEnemie()
    {
        return InstantiateEnemy<TeleportingEnemy>(
            prefabEnemiesTeleporting[Random.Range(0, prefabEnemiesTeleporting.Count)]);
    }
}
