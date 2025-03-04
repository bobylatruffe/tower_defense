using System;
using UnityEngine;

public class Player : A_Player
{
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
        Mediator = GameManager.Instance;

        LifePoints = 100;
        Money = 500;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            showMenu();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Mediator.onEventFromManagers(
                new Tuple<EventTypeFromManager, object>(EventTypeFromManager.ADD_MONEY_TO_PLAYER,
                    500));
        }
    }

    public override int removeLifePoint(int pointsToRemove)
    {
        LifePoints -= pointsToRemove;
        if (LifePoints <= 0)
        {
            Mediator.onEventFromManagers(
                new Tuple<EventTypeFromManager, object>(EventTypeFromManager.PLAYER_IS_DEATH, null));
        }

        return LifePoints;
    }

    public override int removeMoney(int moneyToRemove)
    {
        Money -= moneyToRemove;
        return Money;
    }

    public override int addMoney(int moneyToAdd)
    {
        Money += moneyToAdd;
        return Money;
    }
}
