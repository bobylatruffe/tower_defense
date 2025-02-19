using UnityEngine;

public abstract class A_TowerFactory : MonoBehaviour
{
    public abstract A_Tower createTower(string towerName);
}
