using UnityEngine;

public abstract class A_WaveManager : MonoBehaviour
{
    protected I_GameManagerMediator Mediator { get; private set; }
    protected I_EnemieAbstractFactory EnemieFactory { get; private set; }

    public int CurrentLevel { get; set; }

    private void Awake()
    {
        Mediator = GameManager.Instance;
    }

    public abstract void startWave();
}