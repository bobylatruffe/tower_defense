using UnityEngine;

public class NoMoney : I_Event
{
    public object execute(object eventData)
    {
        Debug.Log("No Money");
        return true;
    }
}
