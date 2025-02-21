using System.Collections;
using UnityEngine;

public class Tower : A_Tower
{
    private void Start()
    {
        Strategy = GetComponent<ShootingStrategy>();
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