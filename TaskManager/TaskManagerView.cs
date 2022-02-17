namespace TaskManager;

public abstract class TaskManagerView
{
    // TODO change to real controllers
    private TestController _controller;

    protected TestController Controller => _controller;

    protected TaskManagerView(TestController controller)
    {
        _controller = controller;
    }
    
    public abstract void StartMenu();
}