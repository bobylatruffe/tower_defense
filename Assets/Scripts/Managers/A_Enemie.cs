public abstract class A_Enemie
{
    private int currentHealth;
    private int speed;
    private I_MoveStrategy specialMove;

    public abstract void move();

    public void addSpecialMove(I_MoveStrategy specialMove)
    {
        this.specialMove = specialMove;
    }
}
