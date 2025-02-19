using UnityEngine;

public abstract class A_Tower : MonoBehaviour
{
    protected I_TowerState towerState;
    public abstract void setState(I_TowerState state);
    public abstract void shoot();
}