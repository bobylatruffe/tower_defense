using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour, I_GameManagerMediator, I_UIObserver
{
    public static GameManager Instance { get; private set; }

    private A_Gameboard gameboard;
    private A_Wave wave;
    private A_Player player;
    internal I_SystemObserver systemObserver;
    private A_Shop shop;
    private I_State state;

    private Dictionary<EventTypeFromManager, I_Event> events = new Dictionary<EventTypeFromManager, I_Event>();

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

    public void changeState(I_State newState)
    {
        state = newState;
        state.start();
    }

    private void registerEventHandlers(EventTypeFromManager eventType, I_Event eventHandler)
    {
        events[eventType] = eventHandler;
    }

    private void Start()
    {
        gameboard = A_Gameboard.Instance;
        wave = A_Wave.Instance;
        player = A_Player.Instance;
        systemObserver = MySystem.Instance;
        shop = A_Shop.Instance;

        state = new ShopTime(this);

        registerEventHandlers(EventTypeFromManager.ADD_NEW_ENEMY, new AddNewEnemy(gameboard));
        registerEventHandlers(EventTypeFromManager.GET_EXIT_ENEMY_POINT, new GetExitEnemyPoint(gameboard));
        registerEventHandlers(EventTypeFromManager.GET_ENTRY_ENEMY_POINT, new GetExitEnemyPoint(gameboard));
        registerEventHandlers(EventTypeFromManager.SHOW_MAIN_MENU, new ShowMainMenu(systemObserver));
        registerEventHandlers(EventTypeFromManager.REMOVE_LIFE, new RemoveLife(player, systemObserver));
        registerEventHandlers(EventTypeFromManager.UPDATE_LIFE_POINTS, new UpdateLifePoint(systemObserver));
        registerEventHandlers(EventTypeFromManager.UPDATE_MONEY, new UpdateMoney(systemObserver));
        registerEventHandlers(EventTypeFromManager.SHOW_TOWER_SHOP, new UpdateMoney(systemObserver));
        registerEventHandlers(EventTypeFromManager.REMOVE_MONEY, new RemoveMoney(systemObserver, player));
        registerEventHandlers(EventTypeFromManager.NO_MONEY, new NoMoney());
        registerEventHandlers(EventTypeFromManager.ADD_MONEY_TO_PLAYER,
            new AddMoneyToPlayer(player, systemObserver));
        registerEventHandlers(EventTypeFromManager.UPDATE_LEVEL_HUD, new UpdateLevelHud(systemObserver));
        registerEventHandlers(EventTypeFromManager.UPDATE_TIMER_BEFORE_WAVE, new UpdateTimerBeforeWave(systemObserver));
        registerEventHandlers(EventTypeFromManager.HIDE_TIMER_BEFORE_WAVE, new HideTimerBeforeWave(systemObserver));
        registerEventHandlers(EventTypeFromManager.GET_ALL_ENEMIES, new GetAllEnemies(gameboard));
    }

    public void start()
    {
        systemObserver.onEvent(new Tuple<string, object>("UPDATE_LIFE_POINTS", player.LifePoints));
        systemObserver.onEvent(new Tuple<string, object>("UPDATE_MONEY", player.Money));

        state.start();
    }

    public void end()
    {
    }

    public object onEventFromManagers(Tuple<EventTypeFromManager, object> eventFromManager)
    {
        if (events.TryGetValue(eventFromManager.Item1, out I_Event command))
        {
            return command.execute(eventFromManager.Item2);
        }

        switch (eventFromManager.Item1)
        {
            case EventTypeFromManager.PLAYER_IS_DEATH:
                changeState(new DeathTime(this));
                break;

            default:
                Debug.Log("Aucune commande pour l'event " + eventFromManager.Item1);
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
                A_Tower tower = shop.buyIfPlayerCanAffordIt(player.Money, nameTowerSelectedByUser);
                gameboard.addTower(tower);
                break;

            case "START_GAME":
                start();
                break;

            case "BUY_TOWER_FINISHED":
                state.end();
                break;
        }
    }
}
