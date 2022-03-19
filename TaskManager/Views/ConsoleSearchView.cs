using System.Data;

namespace TaskManager.Views;

public class ConsoleSearchView : ConsoleView, ISearchView
{
    private static readonly string PROMPT = "Please choose a property to search by:";
    private readonly MainView _mainView;
    private readonly string[] _searchersList;
    private readonly string[] _options;

    public ConsoleSearchView(MainView mainView)
    {
        _mainView = mainView;
        _searchersList = TestController.Instance.GetSearchers();
        _options = CreateOptions();
    }

    public override void Start()
    {
        ConsoleMenu searchMenu = new ConsoleMenu(PROMPT, _options);
        int selectedIndex = searchMenu.Run();
        ApplyAction(selectedIndex);
    }

    private void ApplyAction(int selectedIndex)
    {
        if (selectedIndex == _options.Length - 2)
        {
            Start();
        }
        else if (selectedIndex == _options.Length - 1)
        {
            _mainView.ShowTasksView();
        }
        else
        {
            Console.WriteLine("Please enter a to search by:");
            string param = Console.ReadLine() ?? throw new InvalidExpressionException("Invalid value!");
            List<Task> results = TestController.Instance.Search(_searchersList[selectedIndex], param);
            _mainView.ShowSearchResultsView(results);
        }
    }

    private string[] CreateOptions()
    {
        string[] options = new string[_searchersList.Length + 2];
        int count = 0;
        for (; count < options.Length - 2; count++)
        {
            options[count] = _searchersList[count];
        }

        options[count++] = "";
        options[count] = "Back";
        return options;
    }
}