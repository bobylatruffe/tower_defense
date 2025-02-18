using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class A_ShopManager : MonoBehaviour
{
    private I_GameManagerMediator mediator;
    private I_TowerFactory towerFactory;

    public abstract I_Tower buyTower(String towerName);
}