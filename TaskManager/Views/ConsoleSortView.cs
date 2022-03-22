using System.Data;
using TaskManager.TaskCollections;

namespace TaskManager.Views;

public class ConsoleSortView : SortView
{
    private static readonly string PROMPT = "Please choose a property to sort by:";
    public ConsoleSortView(MainView mainView) : base(mainView)
    {
    }

    public override void Start()
    {
        SortersList = TaskManagerController.Instance.GetSorters();
        string[] options = CreateOptions();
        ConsoleMenu sortMenu = new ConsoleMenu(PROMPT, options);
        int selectedIndex = sortMenu.Run();
        ApplyAction(selectedIndex, options);
    }
    
    private void ApplyAction(int selectedIndex, string[] options)
    {
        if (selectedIndex == options.Length - 2)
        {
            Start();
        }
        else if (selectedIndex == options.Length - 1)
        {
            MainView.ShowTasksView();
        }
        else
        {
            ITaskCollection results = TaskManagerController.Instance.Sort(SortersList![selectedIndex]);
            MainView.ShowSortResultsView(results);
        }
    }
    
    private string[] CreateOptions()
    {
        string[] options = new string[SortersList!.Length + 2];
        int count = 0;
        for (; count < options.Length - 2; count++)
        {
            options[count] = SortersList![count];
        }

        options[count++] = "";
        options[count] = "Back";
        return options;
    }
}