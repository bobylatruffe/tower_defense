public class GetExitEnemyPoint : I_Event
{
    private A_Gameboard gameboard;

    public GetExitEnemyPoint(A_Gameboard gameboard)
    {
        this.gameboard = gameboard;
    }

    public object execute(object eventData)
    {
        return gameboard.getLeave();
    }
}
