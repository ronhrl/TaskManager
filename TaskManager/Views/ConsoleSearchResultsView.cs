namespace TaskManager.Views;

public class ConsoleSearchResultsView : ConsoleView, ISearchResultsView
{
    private static readonly string PROMPT = "Here are your results:";
    private readonly MainView _mainView;
    private List<Task>? _results;

    public ConsoleSearchResultsView(MainView mainView, List<Task>? results = null)
    {
        _mainView = mainView;
        _results = results;
    }

    public override void Start()
    {
        if (_results == null || _results.Count == 0)
        {
            Console.WriteLine("No results!");
            Thread.Sleep(ERROR_MESSAGE_WAIT_TIME);
            _mainView.ShowSearchView();
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
            _mainView.ShowTasksView();
        }
        else
        {
            _mainView.ShowTaskView(_results![selectedIndex]);
        }
    }

    public void SetResults(List<Task> results)
    {
        _results = results;
        // _options = CreateOptions();
    }

    private string[] CreateOptions()
    {
        if (_results == null)
        {
            throw new InvalidOperationException("No results passed!");
        }
        string[] options = new string[_results.Count + 2];
        
        int count = 0;
        for (; count < options.Length - 2; count++)
        {
            options[count] = _results[count].Title;
        }

        options[count++] = "";
        options[count] = "Back to all tasks";
        
        return options;
    }
}