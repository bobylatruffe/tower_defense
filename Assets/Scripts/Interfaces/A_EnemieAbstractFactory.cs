using UnityEngine;

public abstract class A_EnemieAbstractFactory : MonoBehaviour
{
    public abstract A_Enemie createWalkingEnemie();
    public abstract A_Enemie createFlyingEnemie();
    public abstract A_Enemie createTeleportingEnemie();
}