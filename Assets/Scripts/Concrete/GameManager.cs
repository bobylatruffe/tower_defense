using System;
using UnityEngine;

public class GameManager : MonoBehaviour, I_UIObserver, I_GameManagerMediator
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private A_GameboardManager gameboardManager;
    [SerializeField] private A_WaveManager waveManager;

    private A_ShopManager ShopManager { get; set; }
    private A_PlayerManager PlayerManager { get; set; }
    private I_SystemObserver SystemObserver { get; set; }

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
        if (gameboardManager != null) return;
        gameboardManager = FindFirstObjectByType<A_GameboardManager>();

        if (gameboardManager != null) return;
        GameObject go = new GameObject("Gameboard");
        gameboardManager = go.AddComponent<SimpleGameboard>();

        if (waveManager != null) return;
        waveManager = FindFirstObjectByType<A_WaveManager>();

        if (waveManager != null) return;
        go = new GameObject("WaveManager");
        waveManager = go.AddComponent<SimpleWaveManager>();
    }

    public void start()
    {
    }

    public void end()
    {
    }

    public void onEventFromUI(object data)
    {
        throw new NotImplementedException();
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


            default:
                throw new NotImplementedException();
        }

        return null;
    }
}