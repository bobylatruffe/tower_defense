using System.Collections;
using UnityEngine;
using System.Linq;

public class BaseTowerDecorator : A_Tower
{
    protected A_Tower Wrappee { get; set; }
    private Coroutine shootingCoroutine;

    protected virtual void Start()
    {
        A_Tower firstEnabledTower = gameObject.GetComponents<A_Tower>().FirstOrDefault(tower => tower.enabled);
        Wrappee = firstEnabledTower;

        if (firstEnabledTower != null)
        {
            firstEnabledTower.enabled = false;
        }
    }

    public override void shoot()
    {
        // Wrappee.shoot();
        if (shootingCoroutine == null)
        {
            shootingCoroutine = StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.5f);
        if (Wrappee != null)
        {
            Wrappee.shoot();
        }

        shootingCoroutine = null;
    }
}