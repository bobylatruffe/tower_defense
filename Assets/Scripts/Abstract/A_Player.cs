using System;
using UnityEngine;

public abstract class A_Player : MonoBehaviour
{
    public static A_Player Instance { get; protected set; }

    protected I_GameManagerMediator Mediator { get; set; }

    public int LifePoints { get; set; }
    public int Money { get; set; }


    public void showMenu()
    {
        Mediator.onEventFromManagers(
            new Tuple<EventTypeFromManager, object>(EventTypeFromManager.SHOW_PAUSE_MENU, null));
    }

    public abstract int removeLifePoint(int pointsToRemove);
    public abstract int removeMoney(int moneyToRemove);
    public abstract int addMoney(int moneyToAdd);
}
