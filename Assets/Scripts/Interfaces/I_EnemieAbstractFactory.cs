using UnityEngine;

public interface I_EnemieAbstractFactory
{
    A_Enemie createWalkingEnemie();
    A_Enemie createFlyingEnemie();
    A_Enemie createTeleportingEnemie();
}