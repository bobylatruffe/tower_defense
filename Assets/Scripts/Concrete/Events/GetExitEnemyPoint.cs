public class GetExitEnemyPoint : I_Event
{
    private A_GameboardManager gameboard;

    public GetExitEnemyPoint(A_GameboardManager gameboard)
    {
        this.gameboard = gameboard;
    }

    public object execute(object eventData)
    {
        return gameboard.getLeave();
    }
}
