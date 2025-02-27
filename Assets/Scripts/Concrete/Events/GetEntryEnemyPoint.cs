public class GetEntryEnemyPoint : I_Event
{
    private A_Gameboard gameboard;

    public GetEntryEnemyPoint(A_Gameboard gameboard)
    {
        this.gameboard = gameboard;
    }
    public object execute(object eventData)
    {
        return gameboard.getEntry();
    }
}
