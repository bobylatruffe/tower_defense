using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleWaveManager : A_WaveManager
{
    private List<Type> walkingMoveStrategies = new List<Type>
    {
        typeof(SimpleWalkingToDestinationStrategy),
        // typeof(SimpleWalkingToDestinationHighSpeedStrategy),
        // typeof(SimpleWalkingRodingStrategy)
    };

    private List<Type> flyingMoveStrategies = new List<Type>
    {
        // typeof(SimpleFlyingStrategy),
    };

    private int timerBeforeWave = 3;
    private float inistialSpawnDuraction = 5f;
    private float waitTimeBeforeSpawnOneEnemy = 0.5f;
    private float currentSpawnDuration;

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
        Mediator = GameManager.Instance;
        EnemyAbstractFactory = FindFirstObjectByType<SimpleAEnemieFactory>();
        CurrentLevel = 1;
        currentSpawnDuration = inistialSpawnDuraction;
    }

    public override void startWave()
    {
        Mediator.onEventFromManagers(new Tuple<string, object>("UPDATE_LEVEL_HUD", CurrentLevel));
        StartCoroutine(ManageWave());
    }

    private IEnumerator ManageWave()
    {
        while (true)
        {
            yield return StartCoroutine(TimerBeforeWave(timerBeforeWave));
            Mediator.onEventFromManagers(new Tuple<string, object>("UPDATE_LEVEL_HUD", CurrentLevel));
            yield return StartCoroutine(SpawnEnemies(
                currentSpawnDuration += 1,
                Mathf.Max(waitTimeBeforeSpawnOneEnemy -= 0.05f, 0.1f)));

            Mediator.onEventFromManagers(new Tuple<string, object>("ADD_MONEY_PLAYER", 500));
        }
    }

    private IEnumerator TimerBeforeWave(int timerBeforeWave)
    {
        while (A_GameboardManager.Instance.Enemies.Count > 0)
            yield return new WaitForSeconds(0.1f);

        while (timerBeforeWave > 0)
        {
            Mediator.onEventFromManagers(new Tuple<string, object>("UPDATE_TIMER_BEFORE_WAVE", timerBeforeWave));
            timerBeforeWave -= 1;
            yield return new WaitForSeconds(1f);
        }

        yield return Mediator.onEventFromManagers(new Tuple<string, object>("HIDE_TIMER_BEFORE_WAVE", null));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale *= 2;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Time.timeScale /= 2;
        }

        if (Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i < 10; i++) SpawnWalkingEnemy();
            // StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies(float spawnDuration, float waitTime)
    {
        float elapsedTime = 0f;
        Debug.Log(waitTime);

        while (elapsedTime < spawnDuration)
        {
            switch (Random.Range(0, 1))
            {
                case 0:
                    SpawnWalkingEnemy();
                    // SpawnFlyingEnemy();
                    break;
                case 1:
                    // SpawnWalkingEnemy();
                    // SpawnTeleportingEnemy();
                    // SpawnFlyingEnemy();
                    break;
                case 2:
                    // SpawnWalkingEnemy();
                    // SpawnFlyingEnemy();
                    break;
            }

            // float waitTime = Random.Range(0.1f, 0.5f);
            // yield return new WaitForSeconds(Random.Range(waitTime - 0.5f, waitTime + 0.5f));
            yield return new WaitForSeconds(waitTime);

            elapsedTime += waitTime;
        }

        CurrentLevel++;
    }

    private void SpawnWalkingEnemy()
    {
        A_Enemie enemy = EnemyAbstractFactory.createWalkingEnemie();

        Type randomStrategyType = walkingMoveStrategies[Random.Range(0, walkingMoveStrategies.Count)];
        I_MoveStrategy strategy = (I_MoveStrategy)enemy.gameObject.AddComponent(randomStrategyType);

        strategy.setDestination(Mediator.onEventFromManagers(new Tuple<string, object>("GET_LEAVE", null)));
        enemy.MoveStrategy = strategy;
        Mediator.onEventFromManagers(new Tuple<string, object>("ADD_NEW_ENEMY", enemy));
    }

    private void SpawnTeleportingEnemy()
    {
        A_Enemie enemy = EnemyAbstractFactory.createTeleportingEnemie();
        Mediator.onEventFromManagers(new Tuple<string, object>("ADD_NEW_ENEMY", enemy));
    }

    private void SpawnFlyingEnemy()
    {
        A_Enemie enemy = EnemyAbstractFactory.createFlyingEnemie();

        Type randomStrategyType = flyingMoveStrategies[Random.Range(0, flyingMoveStrategies.Count)];
        I_MoveStrategy strategy = (I_MoveStrategy)enemy.gameObject.AddComponent(randomStrategyType);

        strategy.setDestination(Mediator.onEventFromManagers(new Tuple<string, object>("GET_LEAVE", null)));
        enemy.MoveStrategy = strategy;

        Mediator.onEventFromManagers(new Tuple<string, object>("ADD_NEW_ENEMY", enemy));
    }
}
