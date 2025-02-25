using System;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour, I_GameManagerMediator, I_UIObserver
{
    public static GameManager Instance { get; private set; }

    private A_GameboardManager gameboardManager;
    private A_WaveManager waveManager;
    private A_PlayerManager playerManager;
    internal I_SystemObserver systemObserver;
    private A_ShopManager shopManager;

    public I_State state;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void changeState(I_State newState)
    {
        state = newState;
        state.start();
    }

    private void Start()
    {
        gameboardManager = A_GameboardManager.Instance;
        waveManager = A_WaveManager.Instance;
        playerManager = A_PlayerManager.Instance;
        systemObserver = MySystem.Instance;
        shopManager = A_ShopManager.Instance;

        state = new ShopTime(this);
    }

    public void start()
    {
        systemObserver.onEvent(new Tuple<string, object>("UPDATE_LIFE_POINTS", playerManager.LifePoints));
        systemObserver.onEvent(new Tuple<string, object>("UPDATE_MONEY", playerManager.Money));

        state.start();
    }

    public void end()
    {
    }

    public GameObject onEventFromManagers(Tuple<string, object> eventData)
    {
        switch (eventData.Item1)
        {
            case "ADD_NEW_ENEMY":
                gameboardManager.addEnemie((A_Enemie)eventData.Item2);
                break;

            case "GET_LEAVE":
                return gameboardManager.getLeave();

            case "GET_ENTRY":
                return gameboardManager.getEntry();

            case "SHOW_MAIN_MENU":
                systemObserver.onEvent(new Tuple<string, object>("SHOW_MAIN_MENU", null));
                break;

            case "REMOVE_LIFE":
                int lifePointsToRemmove = (int)eventData.Item2;
                int newLifePoints = playerManager.removeLifePoint(lifePointsToRemmove);
                systemObserver.onEvent(new Tuple<string, object>("UPDATE_LIFE_POINTS", newLifePoints));
                break;

            case "UPDATE_LIFE_POINTS":
                systemObserver.onEvent(new Tuple<string, object>("UPDATE_LIFE_POINTS", eventData.Item2));
                break;

            case "UPDATE_MONEY":
                systemObserver.onEvent(new Tuple<string, object>("UPDATE_MONEY", eventData.Item2));
                break;

            case "SHOW_TOWER_SHOP":
                systemObserver.onEvent(new Tuple<string, object>("SHOW_TOWER_SHOP", null));
                break;

            case "REMOVE_MONEY":
                int moneyToRemove = (int)eventData.Item2;
                int newMoneyValue = playerManager.removeMoney(moneyToRemove);
                systemObserver.onEvent(new Tuple<string, object>("UPDATE_MONEY", newMoneyValue));
                break;

            case "NO_MONEY":
                Debug.Log("NO MONEY");
                break;

            case "ADD_MONEY_PLAYER":
                int moneyToAdd = (int)eventData.Item2;
                systemObserver.onEvent(new Tuple<string, object>("UPDATE_MONEY", playerManager.addMoney(moneyToAdd)));
                break;

            case "UPDATE_LEVEL_HUD":
                int currentLevel = (int)eventData.Item2;
                systemObserver.onEvent(new Tuple<string, object>("UPDATE_LEVEL_HUD", currentLevel));
                break;

            case "UPDATE_TIMER_BEFORE_WAVE":
                int currentTimer = (int)eventData.Item2;
                systemObserver.onEvent(new Tuple<string, object>("UPDATE_TIMER_BEFORE_WAVE", currentTimer));
                break;

            case "HIDE_TIMER_BEFORE_WAVE":
                systemObserver.onEvent(new Tuple<string, object>("HIDE_TIMER_BEFORE_WAVE", null));
                break;
        }

        return null;
    }

    public void onEventFromUI(Tuple<string, object> dataEvent)
    {
        switch (dataEvent.Item1)
        {
            case "TOWER_SELECTED_FROM_HUD":
                string nameTowerSelectedByUser = dataEvent.Item2 as string;
                A_Tower tower = shopManager.buyIfPlayerCanAffordIt(playerManager.Money, nameTowerSelectedByUser);
                gameboardManager.addTower(tower);
                break;

            case "START_GAME":
                start();
                break;

            case "BUY_TOWER_FINISHED":
                state.end();
                break;
        }
    }
}
