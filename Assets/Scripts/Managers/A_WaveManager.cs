public abstract class A_WaveManager
{
    private I_GameManagerMediator mediator;
    private I_EnemieAbstractFactory enemieFactory;

    public int CurrentLevel { get; set; }

    public abstract void startWave();
}
