using System;
using System.Collections.Generic;
using UnityEngine;

public class MySystem : MonoBehaviour, I_SystemObserver
{
    private static MySystem Instance { get; set; }

    private I_SoundManager SoundManager { get; set; }

    [SerializeField] private A_HudManager hudManager;
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

        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<GameManager>();
            if (gameManager == null)
            {
                GameObject go = new GameObject("GameManager");
                gameManager = go.AddComponent<GameManager>();
            }
        }

        if (hudManager == null)
        {
            hudManager = FindFirstObjectByType<A_HudManager>();
            if (hudManager == null)
            {
                GameObject hudGo = new GameObject("HudManager");
                hudManager = hudGo.AddComponent<Hud>();
            }
        }
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

        switch (eventData.Item1)
        {
            case "SHOW_MAIN_MENU":
                hudManager.showMenu();
                break;

            case "UPDATE_LIFE_POINTS":
                int newLifePoints = (int)eventData.Item2;
                hudManager.updateLife(newLifePoints);
                break;
        }
    }
}