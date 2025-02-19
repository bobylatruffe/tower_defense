using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class A_ShopManager : MonoBehaviour
{
    public static A_ShopManager Instance { get; protected set; }

    protected I_GameManagerMediator Mediator { get; set; }
    protected I_TowerFactory TowerFactory { get; set; }

    public abstract A_Tower buyIfPlayerCanAffordIt(int playerMoney, string towerName);
}