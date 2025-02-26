public class GetAllEnemies : I_Event
{
    private A_GameboardManager gameboard;

    public GetAllEnemies(A_GameboardManager gameboard)
    {
        this.gameboard = gameboard;
    }

    public object execute(object eventData)
    {
        return gameboard.Enemies;
    }
}
