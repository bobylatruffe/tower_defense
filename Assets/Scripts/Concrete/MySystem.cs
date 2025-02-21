using System;
using System.Collections.Generic;
using UnityEngine;

public class MySystem : MonoBehaviour, I_SystemObserver
{
    public static MySystem Instance { get; private set; }

    private I_SoundManager SoundManager { get; set; }

    private A_HudManager hudManager;
    private GameManager gameManager;

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
        hudManager = A_HudManager.Instance;
        gameManager = GameManager.Instance;
        Application.targetFrameRate = 50;
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

            case "SHOW_TOWER_SHOP":
                hudManager.showTowerShop();
                break;

            case "UPDATE_MONEY":
                int money = (int)eventData.Item2;
                hudManager.updateMoney(money);
                break;
        }
    }
}