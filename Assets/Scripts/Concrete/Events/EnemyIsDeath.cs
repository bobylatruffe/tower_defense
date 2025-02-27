using UnityEngine;

public class EnemyIsDeath : I_Event
{
    private A_Gameboard gameboard;

    public EnemyIsDeath(A_Gameboard gameboard)
    {
        this.gameboard = gameboard;
    }

    public object execute(object eventData)
    {
        Debug.Log(eventData.GetType());
        gameboard.enemyIsDeath((GameObject)eventData);
        return true;
    }
}
