using UnityEngine;

public abstract class A_WaveManager : MonoBehaviour
{
    protected I_GameManagerMediator Mediator { get; private set; }
    [SerializeField] private A_EnemieAbstractFactory enemyAbstractFactory;

    public int CurrentLevel { get; set; }

    private void Awake()
    {
        Mediator = GameManager.Instance;
    }

    private void Start()
    {
        if (enemyAbstractFactory != null) return;
        enemyAbstractFactory = FindFirstObjectByType<A_EnemieAbstractFactory>();

        if (enemyAbstractFactory != null) return;
        GameObject obj = new GameObject("AbstractEnemieFactory");
        obj.transform.SetParent(transform);

        obj.AddComponent<SimpleAEnemieFactory>();

        enemyAbstractFactory = obj.GetComponent<A_EnemieAbstractFactory>();
    }


    public abstract void startWave();
}