using UnityEngine;

public abstract class A_Enemie : MonoBehaviour
{
    protected int CurrentHealth { get; set; }
    protected int Speed { get; set; }
    protected I_MoveStrategy SpecialMove { get; set; }

    protected virtual void move()
    {
        transform.Translate(Vector3.forward * 1 * Time.deltaTime);
    }

    public void addSpecialMove(I_MoveStrategy specialMove)
    {
        SpecialMove = specialMove;
    }
}