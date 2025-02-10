using System;
using System.Collections.Generic;

public class MySystem : I_SystemObserver
{
    public static MySystem Instance { get; private set; }

    private A_HudManager hudManager;
    private I_SoundManager soundManager;
    private GameManager gameManager;
    private List<I_Logger> loggers;

    private MySystem(A_HudManager hudManager, I_SoundManager soundManager, GameManager gameManager)
    {
        this.hudManager = hudManager;
        this.soundManager = soundManager;
        this.gameManager = gameManager;
    }

    public static MySystem Initialize(A_HudManager hudManager, I_SoundManager soundManager, GameManager gameManager)
    {
        if (Instance == null)
        {
            Instance = new MySystem(hudManager, soundManager, gameManager);
        }
        else
        {
            throw new InvalidOperationException("MySystem is already initialized!");
        }

        return Instance;
    }

    public void addLogger(I_Logger logger)
    {
        loggers.Add(logger);
    }

    public void onEvent(Tuple<string, object> eventData)
    {
        foreach (var loggeer in loggers)
        {
            loggeer.log(eventData.Item1);
        }
    }
}
