using System.Collections.Generic;
using UnityEngine;

public class SimpleAEnemieFactory : A_EnemieAbstractFactory
{
    [SerializeField] private List<GameObject> prefabEnemiesWalking = new List<GameObject>();
    [SerializeField] private List<GameObject> prefabEnemiesFlying = new List<GameObject>();
    [SerializeField] private List<GameObject> prefabEnemiesTeleporting = new List<GameObject>();

    public override A_Enemie createWalkingEnemie()
    {
        throw new System.NotImplementedException();
    }

    public override A_Enemie createFlyingEnemie()
    {
        throw new System.NotImplementedException();
    }

    public override A_Enemie createTeleportingEnemie()
    {
        throw new System.NotImplementedException();
    }
}