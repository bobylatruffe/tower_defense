public class AddNewEnemy : I_Event
{
    private A_Gameboard gameboard;

    public AddNewEnemy(A_Gameboard gameboard)
    {
        this.gameboard = gameboard;
    }

    public object execute(object eventData)
    {
        return gameboard.addEnemie((A_Enemie)eventData);
    }
}
