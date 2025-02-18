using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class A_HudManager : MonoBehaviour
{
    private I_UIObserver uiObserver;

    public abstract void udpateLevel(int level);
    public abstract void updateMoney(int money);
    public abstract void updateLife(int life);
    public abstract void showTowerShop(List<(string name, int price)> towersDescription);
    public abstract void showMenu();
    public abstract void showError();
    public abstract void sendUIEvent(Tuple<string, int> eventData);

    private void Start()
    {
        uiObserver = GameManager.Instance;
    }
}