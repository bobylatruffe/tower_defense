using System;
using System.Collections.Generic;

public abstract class A_ShopManager
{
    private I_GameManagerMediator mediator;
    private I_TowerFactory towerFactory;

    public abstract I_Tower buyTower(Tuple<string, int> towerDescription);
    public abstract void sendTowersAvailableDescription();
}
