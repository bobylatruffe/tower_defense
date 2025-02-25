using UnityEngine;

public class WaveTime : I_State
{
    private GameManager gameManager;

    public WaveTime(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void start()
    {
        A_WaveManager.Instance.startWave(end);
    }

    public void end()
    {
        gameManager.changeState(new ShopTime(gameManager));
    }
}
