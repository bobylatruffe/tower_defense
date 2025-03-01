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

    public void upgradeTower()
    {
        A_Tower atower = gameObject.transform.parent.gameObject.GetComponent<A_Tower>();
        string atowerName = atower.name.Replace("(Clone)", "");
        GameObject nextUpgradedTower = null;
        for (int i = 0; i < atower.possibleUpgrade.towers.Count; i++)
        {
            if (atower.possibleUpgrade.towers[i].name == atowerName)
            {
                if (i + 1 < atower.possibleUpgrade.towers.Count)
                {
                    nextUpgradedTower = atower.possibleUpgrade.towers[i + 1];
                }
                else
                {
                    Debug.Log("No more upgrades available");
                }
            }
        }

        if (nextUpgradedTower != null)
        {
            A_Tower newATower = A_Shop.Instance.buyIfPlayerCanAffordIt(A_Player.Instance.Money, nextUpgradedTower.name);
            A_Gameboard.Instance.upgradeTower(atower, newATower);
        }
    }
}
