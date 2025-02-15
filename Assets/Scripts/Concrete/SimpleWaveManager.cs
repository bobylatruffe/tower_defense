using System;
using UnityEngine;

public class SimpleWaveManager : A_WaveManager
{
    public override void startWave()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            A_Enemie enemy = enemyAbstractFactory.createWalkingEnemie();
            Mediator.onEventFromManagers(new Tuple<string, object>("ADD_NEW_ENEMY", enemy));
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            A_Enemie enemy = enemyAbstractFactory.createTeleportingEnemie();
            Mediator.onEventFromManagers(new Tuple<string, object>("ADD_NEW_ENEMY", enemy));
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            A_Enemie enemy = enemyAbstractFactory.createFlyingEnemie();
            Mediator.onEventFromManagers(new Tuple<string, object>("ADD_NEW_ENEMY", enemy));
        }
    }
}