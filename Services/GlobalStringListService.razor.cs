public class GlobalStringListService
{
    private List<string> _stringList = new List<string>();

    public IReadOnlyList<string> StringList => _stringList.AsReadOnly();

    public void AddString(string item)
    {
        _stringList.Add(item);
    }

    public void RemoveString(string item)
    {
        _stringList.Remove(item);
    }

    public void ClearList()
    {
        _stringList.Clear();
    }
}