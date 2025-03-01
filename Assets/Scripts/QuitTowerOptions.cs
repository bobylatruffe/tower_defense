using UnityEngine;

public class QuitTowerOptions : MonoBehaviour
{
    private A_Hud hud;

    private void Start()
    {
        hud = A_Hud.Instance;
    }

    public void closeTowerOptions()
    {
        Transform parent = gameObject.transform.parent;

        while (parent != null)
        {
            if (parent.gameObject.name == "towerOptions(Clone)")
            {
                break;
            }

            parent = parent.parent;
        }

        hud.closeTowerOptions(parent.gameObject);
    }
}
