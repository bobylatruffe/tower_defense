using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class Hud : A_Hud
{
    private VisualElement mainMenu;

    private Label nbLife;
    private Label money;
    private Label nbWave;
    private Label timerBeforeWave;

    private VisualElement shopPanel;

    private Button jouer;
    private Button quitter;
    private Button options;
    private Button historique;
    private Button startWave;

    [SerializeField] private Light towerLight;

    private Camera cam;
    private bool isSelectionTower = false;

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
        uiObserver = GameManager.Instance;
        cam = Camera.main;

        VisualElement root = GetComponentInChildren<UIDocument>().rootVisualElement;
        mainMenu = root.Q<VisualElement>("MainMenuPanel");
        nbLife = root.Q<Label>("NbLife");
        money = root.Q<Label>("Money");
        nbWave = root.Q<Label>("NbWave");
        timerBeforeWave = root.Q<Label>("TimerBeforeWave");

        jouer = root.Q<Button>("Jouer");
        quitter = root.Q<Button>("Quitter");
        options = root.Q<Button>("Options");
        historique = root.Q<Button>("Historique");
        startWave = root.Q<Button>("StartWave");

        jouer.RegisterCallback<ClickEvent>(OnJouerClicked);
        startWave.RegisterCallback<ClickEvent>(OnStartWaveClicked);

        shopPanel = root.Q<VisualElement>("ShopPanel");
    }

    private void OnStartWaveClicked(ClickEvent evt)
    {
        showTowerShop();
        uiObserver.onEventFromUI(new Tuple<string, object>("BUY_TOWER_FINISHED", null));
    }

    private void OnJouerClicked(ClickEvent evt)
    {
        uiObserver.onEventFromUI(new Tuple<string, object>("START_GAME", null));
        mainMenu.style.display = DisplayStyle.None;
    }

    private void Update()
    {
        if (isSelectionTower)
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

                if (Input.GetMouseButtonDown(0))
                {
                    uiObserver.onEventFromUI(new Tuple<string, object>("TOWER_SELECTED_FROM_HUD", item.name));
                }
            }

            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
        }
    }


    public override void updateLevel(int level)
    {
        nbWave.text = level.ToString();
    }

    public override void updateMoney(int money)
    {
        this.money.text = $"{money}€";
    }

    public override void updateLife(int life)
    {
        nbLife.text = $"{life}";
    }

    public override void updateTimerBeforeWave(int timer)
    {
        timerBeforeWave.style.display = DisplayStyle.Flex;
        timerBeforeWave.text = timer.ToString();
    }

    public override void showTowerShop()
    {
        isSelectionTower = !isSelectionTower;
        Vector3 transformPosition = cam.transform.position;
        if (transformPosition.x < 16f)
        {
            Camera.main.transform.DOMoveX(20, 0.5f).SetEase(Ease.Flash);
            shopPanel.style.display = DisplayStyle.Flex;
        }
        else
        {
            Camera.main.transform.DOMoveX(12, 0.5f).SetEase(Ease.Flash);
            shopPanel.style.display = DisplayStyle.None;
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

    public override void hideTimerBeforeWave()
    {
        timerBeforeWave.style.display = DisplayStyle.None;
    }
}
