using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class A_HudManager : MonoBehaviour
{
    public static A_HudManager Instance { get; protected set; }

    protected I_UIObserver uiObserver;

    public abstract void udpateLevel(int level);
    public abstract void updateMoney(int money);
    public abstract void updateLife(int life);
    public abstract void showTowerShop();
    public abstract void showMenu();
    public abstract void showError();
    public abstract void sendUIEvent(Tuple<string, int> eventData);
}