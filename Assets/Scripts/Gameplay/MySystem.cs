using System;
using System.Collections.Generic;
using UnityEngine;

public class MySystem : MonoBehaviour, I_SystemObserver
{
    public static MySystem Instance { get; private set; }

    private A_HudManager HudManager { get; set; }
    private I_SoundManager SsoundManager { get; set; }
    private GameManager GameManager { get; set; }
    private List<I_Logger> Loggers { get; set; }

    public void addLogger(I_Logger logger)
    {
        Loggers.Add(logger);
    }

    private void Start()
    {
        if (Instance != null) return;

        Instance = this;
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void onEvent(Tuple<string, object> eventData)
    {
        foreach (I_Logger loggeer in Loggers)
        {
            loggeer.log(eventData.Item1);
        }
    }
}
