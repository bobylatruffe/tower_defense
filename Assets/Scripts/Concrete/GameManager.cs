using System;
using UnityEngine;

public class GameManager : MonoBehaviour, I_UIObserver, I_GameManagerMediator
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private A_GameboardManager gameboardManager;
    [SerializeField] private A_WaveManager waveManager;
    [SerializeField] private A_PlayerManager playerManager;
    [SerializeField] private I_SystemObserver system;
    [SerializeField] private A_ShopManager shopManager;

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
        if (gameboardManager == null)
        {
            gameboardManager = FindFirstObjectByType<A_GameboardManager>();
            if (gameboardManager == null)
            {
                GameObject gameboardGO = new GameObject("Gameboard");
                gameboardManager = gameboardGO.AddComponent<SimpleGameboard>();
            }
        }

        if (waveManager == null)
        {
            waveManager = FindFirstObjectByType<A_WaveManager>();
            if (waveManager == null)
            {
                GameObject waveGO = new GameObject("WaveManager");
                waveManager = waveGO.AddComponent<SimpleWaveManager>();
            }
        }

        if (playerManager == null)
        {
            playerManager = FindFirstObjectByType<A_PlayerManager>();
            if (playerManager == null)
            {
                GameObject waveGO = new GameObject("PlayerManager");
                playerManager = waveGO.AddComponent<SimplePlayer>();
            }
        }

        if (system == null)
        {
            system = FindFirstObjectByType<MySystem>();
            if (system == null)
            {
                GameObject waveGO = new GameObject("MySystem");
                system = waveGO.AddComponent<MySystem>();
            }
        }

        if (shopManager == null)
        {
            shopManager = FindFirstObjectByType<Shop>();
            if (shopManager == null)
            {
                GameObject waveGO = new GameObject("ShopManager");
                shopManager = waveGO.AddComponent<Shop>();
            }
        }
    }

    public void start()
    {
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
                system.onEvent(new Tuple<string, object>("SHOW_MAIN_MENU", null));
                break;

            case "REMOVE_LIFE":
                int lifePointsToRemmove = (int)eventData.Item2;
                int newLifePoints = playerManager.removeLifePoint(lifePointsToRemmove);
                system.onEvent(new Tuple<string, object>("UPDATE_LIFE_POINTS", newLifePoints));
                break;

            case "UPDATE_LIFE_POINT":
                system.onEvent(new Tuple<string, object>("UPDATE_LIFE_POINTS", eventData.Item2));
                break;

            case "UPDATE_MONEY":
                system.onEvent(new Tuple<string, object>("UPDATE_MONEY", eventData.Item2));
                break;

            case "SHOW_TOWER_SHOP":
                system.onEvent(new Tuple<string, object>("SHOW_TOWER_SHOP", null));
                break;


            default:
                throw new NotImplementedException();
        }

        return null;
    }

    public void onEventFromUI(Tuple<string, object> dataEvent)
    {
        switch (dataEvent.Item1)
        {
            case "TOWER_SELECTED_FROM_HUD":
                Debug.Log(dataEvent.Item2);
                break;
        }
    }
}