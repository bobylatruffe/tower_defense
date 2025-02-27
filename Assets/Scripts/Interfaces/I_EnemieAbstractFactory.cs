using UnityEngine;

public interface I_EnemieAbstractFactory
{
    A_Enemy createWalkingEnemie();
    A_Enemy createFlyingEnemie();
    A_Enemy createTeleportingEnemie();
}