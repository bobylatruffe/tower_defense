using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class A_GameboardManager : MonoBehaviour
{
    protected int Rows { get; set; }
    protected int Cols { get; set; }

    protected List<GameObject> Entries { get; set; } = new List<GameObject>();
    protected List<GameObject> Leaves { get; set; } = new List<GameObject>();

    protected I_GameManagerMediator Mediator { get; set; }
    protected List<I_Tower> Towers { get; set; }
    protected List<A_Enemie> Enemies { get; set; }

    private void Awake()
    {
        Mediator = GameManager.Instance;
    }

    public abstract void addEnemie(A_Enemie newEnemie);
    public abstract void addTower(I_Tower newTower);
    public abstract void upgradeTower(I_Tower towerToUpgrade);
}