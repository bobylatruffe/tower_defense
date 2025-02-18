using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hud : A_HudManager
{
    private VisualElement mainMenu;
    private Label nbLife;
    private Label money;

    public void Awake()
    {
        VisualElement root = GetComponentInChildren<UIDocument>().rootVisualElement;
        mainMenu = root.Q<VisualElement>("MainMenuPanel");
        nbLife = root.Q<Label>("NbLife");
        money = root.Q<Label>("Money");
    }

    public override void udpateLevel(int level)
    {
        throw new NotImplementedException();
    }

    public override void updateMoney(int money)
    {
        throw new NotImplementedException();
    }

    public override void updateLife(int life)
    {
        nbLife.text = $"{life}";
    }

    public override void showTowerShop(List<(string name, int price)> towersDescription)
    {
        throw new NotImplementedException();
    }

    public override void showMenu()
    {
        if (mainMenu.style.display == DisplayStyle.None)
        {
            mainMenu.style.display = DisplayStyle.Flex;
        }
        else
        {
            mainMenu.style.display = DisplayStyle.None;
        }
    }

    public override void showError()
    {
        throw new NotImplementedException();
    }

    public override void sendUIEvent(Tuple<string, int> eventData)
    {
        throw new NotImplementedException();
    }
}