using System.Collections.Generic;
using UnityEngine;

public class SimpleAEnemieFactory : A_EnemieAbstractFactory
{
    [SerializeField] private List<GameObject> prefabEnemiesWalking = new List<GameObject>();
    [SerializeField] private List<GameObject> prefabEnemiesFlying = new List<GameObject>();
    [SerializeField] private List<GameObject> prefabEnemiesTeleporting = new List<GameObject>();

    public override A_Enemie createWalkingEnemie()
    {
        return InstantiateEnemy<WalkingEnemy>(prefabEnemiesWalking[Random.Range(0, prefabEnemiesWalking.Count)]);
    }

    public override A_Enemie createFlyingEnemie()
    {
        return InstantiateEnemy<FlyingEnemy>(prefabEnemiesFlying[Random.Range(0, prefabEnemiesFlying.Count)]);
    }

    public override A_Enemie createTeleportingEnemie()
    {
        return InstantiateEnemy<TeleportingEnemy>(
            prefabEnemiesTeleporting[Random.Range(0, prefabEnemiesTeleporting.Count)]);
    }
}