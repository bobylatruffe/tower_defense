using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class A_Gameboard : MonoBehaviour, I_DetectorEnemyWin
{
    public static A_Gameboard Instance {get; protected set;}

    protected int Rows { get; set; }
    protected int Cols { get; set; }

    protected List<GameObject> Entries { get; set; } = new List<GameObject>();
    protected List<GameObject> Leaves { get; set; } = new List<GameObject>();

    protected I_GameManagerMediator Mediator { get; set; }
    protected List<A_Tower> Towers { get; set; } = new List<A_Tower>();
    public List<A_Enemy> Enemies { get; protected set; } = new List<A_Enemy>();

    public abstract bool addEnemie(A_Enemy newEnemy);
    public abstract void addTower(A_Tower tower);
    public abstract void upgradeTower(A_Tower aTowerToUpgrade, A_Tower newTowerUpgraded);
    public abstract GameObject getLeave();
    public abstract GameObject getEntry();
    public abstract void enemyWin(GameObject enemyGo);
}
