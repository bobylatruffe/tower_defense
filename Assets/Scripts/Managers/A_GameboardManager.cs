using System.Collections.Generic;
using UnityEngine;

public abstract class A_GameboardManager : MonoBehaviour
{
    protected int Rows { get; set; }
    protected int Cols { get; set; }
    public I_GameManagerMediator Mediator { get; protected set; }
    public List<I_Tower> Towers { get; private set; }
    public List<A_Enemie> Enemies { get; private set; }

    public abstract void addEnemie(A_Enemie newEnemie);
    public abstract void addTower(I_Tower newTower);
    public abstract void upgradeTower(I_Tower towerToUpgrade);
}
