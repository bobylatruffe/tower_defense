using System;
using System.Collections;
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
        strategy.shoot(A_GameboardManager.Instance.Enemies);
        yield return new WaitForSeconds(1f);
    }
}