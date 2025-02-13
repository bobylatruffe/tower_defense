using System;
using UnityEngine;

public class GameManager : MonoBehaviour, I_UIObserver, I_GameManagerMediator
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private A_GameboardManager gameboardManager;

    private A_WaveManager WaveManager { get; set; }
    private A_ShopManager ShopManager { get; set; }
    private A_PlayerManager PlayerManager { get; set; }
    private I_SystemObserver SystemObserver { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (gameboardManager != null) return;
        gameboardManager = FindFirstObjectByType<A_GameboardManager>();

        if (gameboardManager != null) return;
        GameObject go = new GameObject("Gameboard");
        gameboardManager = go.AddComponent<SimpleGameboard>();
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

    public void onEventFromManagers(Tuple<string, object> eventData)
    {
        throw new NotImplementedException();
    }
}