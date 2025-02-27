using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class TowerDecorateur : BaseTowerDecorator
{
    private I_TowerStrategy strategy;

    protected override void Start()
    {
        base.Start();
        I_TowerStrategy towerStrategy = gameObject.GetComponent<I_TowerStrategy>();

        strategy = (I_TowerStrategy)gameObject.AddComponent(towerStrategy.GetType());
    }

    private void Update()
    {
        shoot();
    }

    public override void shoot()
    {
        base.shoot();
        StartCoroutine(ShootWithStrategy());
    }

    private IEnumerator ShootWithStrategy()
    {
        yield return new WaitForSeconds(1f);

        List<A_Enemy> enemies = (List<A_Enemy>)Mediator.onEventFromManagers(
            new Tuple<EventTypeFromManager, object>(EventTypeFromManager.GET_ALL_ENEMIES, null));

        strategy.shoot(enemies);

        yield return new WaitForSeconds(1f);
    }
}
