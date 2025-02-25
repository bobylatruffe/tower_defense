using System;

public class ShopTime : I_State
{
    private GameManager gameManager;

    public ShopTime(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void start()
    {
        gameManager.systemObserver.onEvent(new Tuple<string, object>("SHOW_TOWER_SHOP", null));
    }

    public void end()
    {
        gameManager.changeState(new WaveTime(gameManager));
    }
}
