public class GetEntryEnemyPoint : I_Event
{
    private A_GameboardManager gameboard;

    public GetEntryEnemyPoint(A_GameboardManager gameboard)
    {
        this.gameboard = gameboard;
    }
    public object execute(object eventData)
    {
        return gameboard.getEntry();
    }
}
