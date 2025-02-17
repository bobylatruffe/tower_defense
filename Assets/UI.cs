using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    private VisualElement root;
    private Button jouer;
    private Button options;
    private Button historique;
    private Button quitter;

    private void Start()
    {
        Time.timeScale = 0f;

        UIDocument uiDocument = GetComponent<UIDocument>();

        root = uiDocument.rootVisualElement;

        quitter = root.Q<Button>("Quitter");
        quitter.RegisterCallback<ClickEvent>(onButtonQuitterlicked);

        jouer = root.Q<Button>("Jouer");
        jouer.RegisterCallback<ClickEvent>(onButtonJouerClicked);
    }

    private void onButtonQuitterlicked(ClickEvent evt)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void onButtonJouerClicked(ClickEvent evt)
    {
        root.style.display = DisplayStyle.None;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (root.style.display == DisplayStyle.None)
            {
                root.style.display = DisplayStyle.Flex;
                Time.timeScale = 0f;
            }
            else
            {
                root.style.display = DisplayStyle.None;
                Time.timeScale = 1f;
            }
        }
    }
}