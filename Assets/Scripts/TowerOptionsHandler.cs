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
        for (int i = 0; i < atower.PossibleUpgrade.towers.Count; i++)
        {
            if (atower.PossibleUpgrade.towers[i].name == atowerName)
            {
                if (i + 1 < atower.PossibleUpgrade.towers.Count)
                {
                    nextUpgradedTower = atower.PossibleUpgrade.towers[i + 1];
                }
                else
                {
                    hud.showPopup("Tu es déjà au max bro!");
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
