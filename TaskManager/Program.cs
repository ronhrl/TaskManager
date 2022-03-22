using TaskManager.Views;

namespace TaskManager;

public class Program
{
    static void Main(string[] args)
    {
        TaskManagerController controller = TaskManagerController.Instance;
        controller.Start();
    }
}