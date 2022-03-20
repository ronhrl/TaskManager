using TaskManager.TaskCollections;

namespace TaskManager.Views;

public class ConsoleSearchResultsView : SearchResultsView
{
    private static readonly string PROMPT = "Here are your results:";
    // private readonly MainView _mainView;
    // private List<Task>? _results;

    // public ConsoleSearchResultsView(MainView mainView, List<Task>? results = null)
    // {
    //     _mainView = mainView;
    //     _results = results;
    // }

    public ConsoleSearchResultsView(MainView mainView, ITaskCollection? results = null) : base(mainView, results)
    {
    }

    public override void Start()
    {
        if (Results == null || Results.Count == 0)
        {
            Console.WriteLine("No results!");
            Thread.Sleep(ConsoleViewUtils.ErrorMessageWaitTime);
            MainView.ShowSearchView();
        }

        string[] options = CreateOptions();
        ConsoleMenu resultsMenu = new ConsoleMenu(PROMPT, options);
        int selectedIndex = resultsMenu.Run();
        ApplyAction(selectedIndex, resultsMenu);
    }

    private void ApplyAction(int selectedIndex, ConsoleMenu menu)
    {
        if (selectedIndex == menu.GetNumOfOptions() - 2) // dummy option
        { 
            Start();
        }
        else if (selectedIndex == menu.GetNumOfOptions() - 1) // back to all tasks option
        {
            MainView.ShowTasksView();
        }
        else
        {
            MainView.ShowTaskView(Results!.GetTaskAtIndex(selectedIndex));
        }
    }

    // public void SetResults(List<Task> results)
    // {
    //     _results = results;
    //     // _options = CreateOptions();
    // }

    private string[] CreateOptions()
    {
        if (Results == null)
        {
            throw new InvalidOperationException("No results passed!");
        }
        string[] options = new string[Results.Count + 2];
        
        int count = 0;
        for (; count < options.Length - 2; count++)
        {
            options[count] = Results.GetTaskAtIndex(count).Title;
        }

        options[count++] = "";
        options[count] = "Back to all tasks";
        
        return options;
    }
}