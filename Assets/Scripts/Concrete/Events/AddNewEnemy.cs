public class AddNewEnemy : I_Event
{
    private A_GameboardManager gameboard;

    public AddNewEnemy(A_GameboardManager gameboard)
    {
        this.gameboard = gameboard;
    }

    public object execute(object eventData)
    {
        return gameboard.addEnemie((A_Enemie)eventData);
    }
}
