using System;
using UnityEngine;

public abstract class A_PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject mediatorGo;
    protected I_GameManagerMediator mediator;

    public int LifePoints { get; set; }
    public int Money { get; set; }

    protected void Start()
    {
        mediator = mediatorGo.GetComponent<GameManager>();
    }

    public void showMenu()
    {
        mediator.onEventFromManagers(new Tuple<string, object>("SHOW_MAIN_MENU", null));
    }

    public void showTowerShop()
    {
        mediator.onEventFromManagers(new Tuple<string, object>("SHOW_TOWER_SHOP", null));
    }

    public abstract int removeLifePoint(int pointsToRemove);
    public abstract int removeMoney(int moneyToRemove);
}