namespace TaskManager.Views;

public interface ISearchResultsView : IView
{
    public void SetResults(List<Task> results);
}