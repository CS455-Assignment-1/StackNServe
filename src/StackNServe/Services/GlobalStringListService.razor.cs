namespace StackNServe.Services
{
    public class GlobalStringListService 
    {
        public List<string> _stringList = new List<string>();
        public event Action? OnChange;

        public IReadOnlyList<string> StringList => _stringList.AsReadOnly();

        public void AddString(string item)
        {
            if (!string.IsNullOrWhiteSpace(item))
            {
                _stringList.Add(item);
                NotifyStateChanged();
            }
        }

        public void RemoveString(string item)
        {
            if (_stringList.Remove(item))
            {
                NotifyStateChanged();
            }
        }

        public virtual void ClearList()
        {
            _stringList.Clear();
            NotifyStateChanged();
        }

        public void NotifyStateChanged() => OnChange?.Invoke();
    }

}