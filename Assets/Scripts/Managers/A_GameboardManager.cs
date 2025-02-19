using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class A_GameboardManager : MonoBehaviour, I_DetectorEnemyWin
{
    public static A_GameboardManager Instance {get; protected set;}

    protected int Rows { get; set; }
    protected int Cols { get; set; }

    protected List<GameObject> Entries { get; set; } = new List<GameObject>();
    protected List<GameObject> Leaves { get; set; } = new List<GameObject>();

    protected I_GameManagerMediator Mediator { get; set; }
    protected List<A_Tower> Towers { get; set; } = new List<A_Tower>();
    protected List<A_Enemie> Enemies { get; set; } = new List<A_Enemie>();

    public abstract void addEnemie(A_Enemie newEnemie);
    public abstract void addTower(A_Tower tower);
    public abstract void upgradeTower(A_Tower aTowerToUpgrade);
    public abstract GameObject getLeave();
    public abstract GameObject getEntry();
    public abstract void enemyWin(GameObject enemyGo);
}