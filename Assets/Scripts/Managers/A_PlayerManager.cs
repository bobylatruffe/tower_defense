using System;
using UnityEngine;

public abstract class A_PlayerManager : MonoBehaviour
{
    public static A_PlayerManager Instance { get; protected set; }

    protected I_GameManagerMediator Mediator { get; set; }

    public int LifePoints { get; set; }
    public int Money { get; set; }


    public void showMenu()
    {
        Mediator.onEventFromManagers(
            new Tuple<EventTypeFromManager, object>(EventTypeFromManager.SHOW_MAIN_MENU, null));
    }

    public void showTowerShop()
    {
        Mediator.onEventFromManagers(
            new Tuple<EventTypeFromManager, object>(EventTypeFromManager.SHOW_TOWER_SHOP, null));
    }

    public abstract int removeLifePoint(int pointsToRemove);
    public abstract int removeMoney(int moneyToRemove);
    public abstract int addMoney(int moneyToAdd);
}
