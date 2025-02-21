using System.Collections;
using UnityEngine;

public class Tower : A_Tower
{
    private void Start()
    {
        Strategy = GetComponent<I_TowerStrategy>();
    }

    private void Update()
    {
        shoot();
    }

    public override void shoot()
    {
        Strategy.shoot(A_GameboardManager.Instance.Enemies);
    }
}