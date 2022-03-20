namespace TaskManager.Views;

public class ConsoleMainView : MainView
{
    public ConsoleMainView(ViewFactory viewFactory) : base(viewFactory)
    {
    }

    public override void Start()
    {
        ShowInitialView();
    }
}