using UnityEngine;
using UnityEditor;

public class DeathTime : I_State
{
    private GameManager gameManager;

    public DeathTime(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void start()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void end()
    {
    }
}
