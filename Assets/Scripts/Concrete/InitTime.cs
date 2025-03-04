using System;

public class InitTime : I_State
{
    private GameManager gameManager;

    public InitTime(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void start()
    {
        Action callback = end;
        gameManager.systemObserver.onEvent(new Tuple<string, object>("SHOW_MAIN_MENU", callback));
    }

    public void end()
    {
        gameManager.changeState(new ShopTime(gameManager));
    }
}
