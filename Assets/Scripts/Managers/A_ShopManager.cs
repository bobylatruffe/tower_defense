using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class A_ShopManager : MonoBehaviour
{
    [SerializeField] protected I_GameManagerMediator mediator;
    [SerializeField] protected I_TowerFactory towerFactory;

    public abstract A_Tower buyTower(String towerName);

    public abstract A_Tower buyIfPlayerCanAffordIt(int playerMoney, string towerName);
}