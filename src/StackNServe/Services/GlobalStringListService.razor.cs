namespace StackNServe
{
    public class GlobalStringListService
    {
        private List<string> _stringList = new List<string>();
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

        public void ClearList()
        {
            _stringList.Clear();
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

}