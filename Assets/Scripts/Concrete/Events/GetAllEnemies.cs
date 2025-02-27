public class GetAllEnemies : I_Event
{
    private A_Gameboard gameboard;

    public GetAllEnemies(A_Gameboard gameboard)
    {
        this.gameboard = gameboard;
    }

    public object execute(object eventData)
    {
        return gameboard.Enemies;
    }
}
