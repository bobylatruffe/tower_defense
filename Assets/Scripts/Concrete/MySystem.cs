using System;
using System.Collections.Generic;
using UnityEngine;

public class MySystem : MonoBehaviour, I_SystemObserver
{
    public static MySystem Instance { get; private set; }

    private I_SoundManager SoundManager { get; set; }

    private A_Hud hud;
    private GameManager gameManager;

    private List<I_Logger> loggers = new List<I_Logger>();
    private I_Logger scoreLogger = new ScoreLogger();

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
        hud = A_Hud.Instance;
        gameManager = GameManager.Instance;
        Application.targetFrameRate = 120;
    }

    public void addLogger(I_Logger logger)
    {
        loggers.Add(logger);
    }

    public object onEvent(Tuple<string, object> eventData)
    {
        foreach (I_Logger logger in loggers)
        {
            logger.log(eventData.Item1);
        }

        switch (eventData.Item1)
        {
            case "SHOW_MAIN_MENU":
                hud.showMenu(eventData.Item2);
                break;

            case "UPDATE_LIFE_POINTS":
                int newLifePoints = (int)eventData.Item2;
                hud.updateLife(newLifePoints);
                break;

            case "SHOW_TOWER_SHOP":
                hud.showTowerShop();
                break;

            case "UPDATE_MONEY":
                int money = (int)eventData.Item2;
                hud.updateMoney(money);
                break;

            case "UPDATE_LEVEL_HUD":
                int currentLevel = (int)eventData.Item2;
                hud.updateLevel(currentLevel);
                break;

            case "UPDATE_TIMER_BEFORE_WAVE":
                hud.updateTimerBeforeWave((int)eventData.Item2);
                break;

            case "HIDE_TIMER_BEFORE_WAVE":
                hud.hideTimerBeforeWave();
                break;

            case "SelectingOnTowerShop":
                hud.isSelectionTower = true;
                break;

            case "ShowPauseMenu":
                hud.showPauseMenu();
                break;

            case "PLAYER_IS_DEATH":
                hud.showDeadScreen();
                break;

            case "SAVE_PSEUDO_AND_NAME_OF_PLAYER":
                scoreLogger.log((string)eventData.Item2);
                break;

            case "GET_SCORES":
                return ((ScoreLogger)scoreLogger).getAllScores();

            case "Notification":
                hud.showPopup(eventData.Item2);
                break;
        }

        return true;
    }
}
