using TaskManager.Models;

namespace TaskManager.Sorters;

public class SorterFactory
{
    private readonly string[] _sortersList;
    public string[] SortersList
    {
        get => _sortersList;
    }

    public SorterFactory()
    {
        _sortersList = new[] { "Label", "Title" };
    }

    public ITaskSorter CreateSorter(string sorterType)
    {
        switch (sorterType)
        {
            case "Priority":
                return new TaskPrioritySorter();
            case "DueTime":
                return new DueTimeSorter();
            case "CreationTime":
                return new CreationTimeSorter();
            default:
                throw new InvalidDataException($"There is no sorter of type {sorterType}!");
        }
    }
}