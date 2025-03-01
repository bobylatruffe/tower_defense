using UnityEngine;

public class TowerOptionsHandler : MonoBehaviour
{
    private A_Hud hud;

    private void Start()
    {
        /* TODO Attention, il faut plutot que je passe par Mediator, car
           utilisation de singleton la !!!
        */
        hud = A_Hud.Instance;
    }

    public void closeTowerOptions()
    {
        hud.closeTowerOptions(gameObject);
    }

    public void addTowerDecorator()
    {
        GameObject tower = gameObject.transform.parent.gameObject;
        tower.AddComponent<TowerDecorateur>();
    }
}
