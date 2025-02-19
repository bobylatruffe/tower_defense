using System;
using UnityEngine;

public class GameManager : MonoBehaviour, I_GameManagerMediator, I_UIObserver
{
    public static GameManager Instance { get; private set; }

    private A_GameboardManager gameboardManager;
    private A_WaveManager waveManager;
    private A_PlayerManager playerManager;
    private I_SystemObserver systemObserver;
    private A_ShopManager shopManager;

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

    private void Start()
    {
        gameboardManager = A_GameboardManager.Instance;
        waveManager = A_WaveManager.Instance;
        playerManager = A_PlayerManager.Instance;
        systemObserver = MySystem.Instance;
        shopManager = A_ShopManager.Instance;
    }

    public void start()
    {
        systemObserver.onEvent(new Tuple<string, object>("UPDATE_LIFE_POINTS", playerManager.LifePoints));
        systemObserver.onEvent(new Tuple<string, object>("UPDATE_MONEY", playerManager.Money));
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
                systemObserver.onEvent(new Tuple<string, object>("SHOW_TOWER_SHOP", null));
                gameboardManager.addTower(tower);
                break;

            case "START_GAME":
                start();
                break;
        }
    }
}