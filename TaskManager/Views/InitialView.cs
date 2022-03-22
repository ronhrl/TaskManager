namespace TaskManager.Views;

public abstract class InitialView : IView
{
    protected readonly MainView MainView;

    protected InitialView(MainView mainView)
    {
        MainView = mainView;
    }

    public abstract void Start();
}