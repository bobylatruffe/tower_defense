using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class Hud : A_HudManager
{
    private VisualElement mainMenu;
    private Label nbLife;
    private Label money;

    [SerializeField] private Light towerLight;
    [SerializeField] private Light towerLight2;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;

        VisualElement root = GetComponentInChildren<UIDocument>().rootVisualElement;
        mainMenu = root.Q<VisualElement>("MainMenuPanel");
        nbLife = root.Q<Label>("NbLife");
        money = root.Q<Label>("Money");
    }

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        float maxDistance = 20;
        int groundLayer = LayerMask.GetMask("Item");

        if (Physics.Raycast(ray, out hit, maxDistance, groundLayer))
        {
            GameObject item = hit.collider.gameObject;
            towerLight.transform.position = new Vector3(towerLight.transform.position.x,
                towerLight.transform.position.y,
                item.transform.position.z);
            towerLight2.transform.position = new Vector3(towerLight2.transform.position.x,
                towerLight2.transform.position.y,
                item.transform.position.z);

            if (Input.GetMouseButtonDown(0))
            {
                uiObserver.onEventFromUI(new Tuple<string, object>("TOWER_SELECTED_FROM_HUD", item.name));
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
    }


    public override void udpateLevel(int level)
    {
        throw new NotImplementedException();
    }

    public override void updateMoney(int money)
    {
        this.money.text = $"{money}€";
    }

    public override void updateLife(int life)
    {
        nbLife.text = $"{life}";
    }

    public override void showTowerShop()
    {
        Vector3 transformPosition = cam.transform.position;
        if (transformPosition.x < 16f)
        {
            Camera.main.transform.DOMoveX(20, 0.5f).SetEase(Ease.Flash);
        }
        else
        {
            Camera.main.transform.DOMoveX(12, 0.5f).SetEase(Ease.Flash);
        }
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