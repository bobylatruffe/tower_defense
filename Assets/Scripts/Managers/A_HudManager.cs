using System.Collections.Generic;

public abstract class A_HudManager
{
    private I_UIObserver uiObserver;

    public abstract void udpateLevel(int level);
    public abstract void showTowerShop(List<(string name, int price)> towersDescription);
    public abstract void showMenu();
    public abstract void showError();

    public void sendUIEvent(object data)
    {
        uiObserver.onEventFromUI(data);
    }
}
