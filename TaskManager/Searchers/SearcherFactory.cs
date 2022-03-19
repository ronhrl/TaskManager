namespace TaskManager;

public class SearcherFactory
{
    private readonly string[] _searchersList;
    public string[] SearchersList
    {
        get => _searchersList;
    }

    public SearcherFactory()
    {
        _searchersList = new[] { "Label", "Title" };
    }

    public ITaskSearcher CreateSearcher(string searcherType)
    {
        switch (searcherType)
        {
            case "Label":
                return new TaskLabelSearcher();
            case "Title":
                return new TaskTitleSearcher();
            default:
                throw new InvalidDataException($"There is no searcher of type {searcherType}!");
        }
    }
}