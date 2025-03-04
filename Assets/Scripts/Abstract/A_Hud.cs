using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class A_Hud : MonoBehaviour
{
    public static A_Hud Instance { get; protected set; }

    protected I_SystemObserver systemObserver;
    protected I_UIObserver uiObserver;
    public bool isSelectionTower { get; set; }

    public abstract void updateLevel(int level);
    public abstract void updateMoney(int money);
    public abstract void updateLife(int life);
    public abstract void updateTimerBeforeWave(int timer);
    public abstract void showTowerShop();
    public abstract void showMenu(object callback);
    public abstract void showError();
    public abstract void hideTimerBeforeWave();
    public abstract void showTowerOptions();
    public abstract void closeTowerOptions(GameObject towerOptions);
    public abstract void showPauseMenu();
    public abstract void closePauseMenu();
    public abstract void quitter();
    public abstract void showScores();
    public abstract void showDeadScreen();
}
