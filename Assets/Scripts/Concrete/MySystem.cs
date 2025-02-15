using System;
using System.Collections.Generic;
using UnityEngine;

public class MySystem : MonoBehaviour, I_SystemObserver
{
    public static MySystem Instance { get; private set; }

    private A_HudManager HudManager { get; set; }
    private I_SoundManager SoundManager { get; set; }

    [SerializeField] private GameManager gameManager;

    private List<I_Logger> loggers = new List<I_Logger>();

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
        if (gameManager != null) return;
        gameManager = FindFirstObjectByType<GameManager>();

        if (gameManager != null) return;
        GameObject go = new GameObject("GameManager");
        gameManager = go.AddComponent<GameManager>();
    }

    public void addLogger(I_Logger logger)
    {
        loggers.Add(logger);
    }

    public void onEvent(Tuple<string, object> eventData)
    {
        foreach (I_Logger logger in loggers)
        {
            logger.log(eventData.Item1);
        }
    }
}