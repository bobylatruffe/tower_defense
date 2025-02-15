using System;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAEnemieFactory : A_EnemieAbstractFactory
{
    [SerializeField] private List<GameObject> prefabEnemiesWalking = new List<GameObject>();
    [SerializeField] private List<GameObject> prefabEnemiesFlying = new List<GameObject>();
    [SerializeField] private List<GameObject> prefabEnemiesTeleporting = new List<GameObject>();

    public override A_Enemie createWalkingEnemie()
    {
        return InstantiateEnemy<WalkingEnemy>(prefabEnemiesWalking[0]);
    }

    public override A_Enemie createFlyingEnemie()
    {
        return InstantiateEnemy<FlyingEnemy>(prefabEnemiesFlying[0]);
    }

    public override A_Enemie createTeleportingEnemie()
    {
        return InstantiateEnemy<TeleportingEnemy>(prefabEnemiesTeleporting[0]);
    }
}