using System.Collections.Generic;

public abstract class A_GamboardManager
{
    private I_GameManagerMediator mediator;
    private List<I_Tower> towers;
    private List<A_Enemie> enemies;

    public abstract void addEnemie(A_Enemie newEnemie);
    public abstract void addTower(I_Tower newTower);
    public abstract void upgradeTower(I_Tower towerToUpgrade);
}
