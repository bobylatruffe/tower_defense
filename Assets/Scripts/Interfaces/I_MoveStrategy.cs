using UnityEngine;

public interface I_MoveStrategy
{
    void move();
    void setDestination(GameObject destination);

    void initStrategy();
}
