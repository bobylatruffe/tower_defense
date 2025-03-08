using System;
using System.Collections.Generic;
using DG.Tweening;
using Michsky.MUIP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI : A_Hud
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject finDeJeu;
    [SerializeField] private GameObject scoresPanel;

    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private TextMeshProUGUI currentLevel;
    [SerializeField] private TextMeshProUGUI life;
    [SerializeField] private TextMeshProUGUI timeBeforeWave;
    [SerializeField] private TextMeshProUGUI currentLevelOfPlayer;

    [SerializeField] private TMP_InputField pseudoInput;
    [SerializeField] private Button startWave;
    [SerializeField] private Light towerLight;

    [SerializeField] private ListView scores;

    [SerializeField] private NotificationManager notificationManager;

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
        systemObserver = MySystem.Instance;
        cam = Camera.main;
        pseudoInput.onSubmit.AddListener(savePlayerPseudoAndLevel);
    }

    private void savePlayerPseudoAndLevel(string pseudo)
    {
        string dataToSave = $"{pseudo}:{currentLevel.text}";
        systemObserver.onEvent(new Tuple<string, object>("SAVE_PSEUDO_AND_NAME_OF_PLAYER", dataToSave));
        pseudoInput.gameObject.SetActive(false);
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

    public override void quitter()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public override void showScores()
    {
        scoresPanel.gameObject.SetActive(true);
        List<(string, string)> pseudoAndLevel =
            (List<(string, string)>)systemObserver.onEvent(new Tuple<string, object>("GET_SCORES", null));

        scores.listItems.Clear();

        foreach ((string pseudo, string score) in pseudoAndLevel)
        {
            ListView.ListItem item = new ListView.ListItem();

            item.row0 = new ListView.ListRow();
            item.row0.rowText = pseudo;
            item.row0.usePreferredWidth = true;

            item.row1 = new ListView.ListRow();
            item.row1.rowText = $"{score}";
            item.row1.usePreferredWidth = true;


            scores.listItems.Add(item);
        }

        scores.rowCount = ListView.RowCount.Two;
        scores.InitializeItems();
    }

    public override void closeScores()
    {
        scoresPanel.gameObject.SetActive(false);
    }

    public override void showDeadScreen()
    {
        finDeJeu.SetActive(true);
        Time.timeScale = 0f;
        currentLevelOfPlayer.text = currentLevel.text;
    }

    public override void showPopup(object eventDataItem2)
    {
        notificationManager.title = "Avetissement";
        notificationManager.description = eventDataItem2.ToString();
        notificationManager.UpdateUI();
        notificationManager.Open();
    }
}
