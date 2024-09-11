namespace StackNServe.Services
{
    public class GlobalStringListService
    {
        public List<string> stringList = new List<string>();
        public event Action? OnChange;

        public IReadOnlyList<string> StringList => stringList.AsReadOnly();

        public void AddString(string item)
        {
            if (!string.IsNullOrWhiteSpace(item))
            {
                stringList.Add(item);
                NotifyStateChanged();
            }
        }

        public void RemoveString(string item)
        {
            if (stringList.Remove(item))
            {
                NotifyStateChanged();
            }
        }

        public virtual void ClearList()
        {
            stringList.Clear();
            NotifyStateChanged();
        }

        public void NotifyStateChanged() => OnChange?.Invoke();
    }

}
