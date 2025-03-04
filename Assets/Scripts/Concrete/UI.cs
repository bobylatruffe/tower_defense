using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UI : A_Hud
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private TextMeshProUGUI currentLevel;
    [SerializeField] private TextMeshProUGUI life;
    [SerializeField] private TextMeshProUGUI timeBeforeWave;

    [SerializeField] private Button startWave;

    [SerializeField] private Light towerLight;

    private Camera cam;

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
    }

    public void OnStartWaveClicked()
    {
        showTowerShop();
        uiObserver.onEventFromUI(new Tuple<string, object>("BUY_TOWER_FINISHED", null));
    }

    public void OnJouerClicked()
    {
        uiObserver.onEventFromUI(new Tuple<string, object>("START_GAME", null));
        mainMenu.SetActive(false);
    }

    public void OnReprendreClicked()
    {
        showPauseMenu();
    }

    public void OnRecommencerClicked()
    {

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
                    isSelectionTower = false;
                    uiObserver.onEventFromUI(new Tuple<string, object>("TOWER_SELECTED_FROM_HUD", item.name));
                }
            }

            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
        }
    }

    public override void updateLevel(int level)
    {
        currentLevel.text = level.ToString();
    }

    public override void updateMoney(int money)
    {
        this.money.text = $"{money}€";
    }

    public override void updateLife(int life)
    {
        this.life.text = $"{life}";
    }

    public override void updateTimerBeforeWave(int timer)
    {
        timeBeforeWave.gameObject.SetActive(true);
        timeBeforeWave.text = timer.ToString();
    }

    public override void showTowerShop()
    {
        isSelectionTower = true;
        Vector3 transformPosition = cam.transform.position;
        if (transformPosition.x < 16f)
        {
            Camera.main.transform.DOMoveX(20, 0.5f).SetEase(Ease.Flash);
            startWave.gameObject.SetActive(true);
        }
        else
        {
            Camera.main.transform.DOMoveX(12, 0.5f).SetEase(Ease.Flash);
            startWave.gameObject.SetActive(false);
        }
    }

    public override void showMenu(object callback)
    {
        mainMenu.SetActive(true);
        ((Action)callback).Invoke();
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
        timeBeforeWave.gameObject.SetActive(false);
    }

    public override void showTowerOptions()
    {
        Debug.Log("bonjour towerOptions");
    }

    public override void closeTowerOptions(GameObject towerOptions)
    {
        towerOptions.gameObject.SetActive(false);
    }

    public override void showPauseMenu()
    {
        if (pauseMenu.activeSelf) closePauseMenu();
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public override void closePauseMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
