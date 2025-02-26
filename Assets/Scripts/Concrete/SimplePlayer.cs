using System;
using UnityEngine;

public class SimplePlayer : A_PlayerManager
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

        LifePoints = 500;
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
            showTowerShop();
        }
    }

    public override int removeLifePoint(int pointsToRemove)
    {
        LifePoints -= pointsToRemove;
        if (LifePoints <= 0)
        {
            Mediator.onEventFromManagers(new Tuple<string, object>("PLAYER_IS_DEATH", null));
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
