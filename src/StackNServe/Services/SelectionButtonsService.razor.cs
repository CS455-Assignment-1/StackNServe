namespace StackNServe
{
    public class SelectionButtonService
    {
        public bool BunSelectVar { get; set; }
        public bool PattySelectVar { get; set; }
        public bool SaucesSelectVar { get; set; }
        public bool ToppingSelectVar { get; set; }

        public event Action OnChange;

        public void NotifyStateChanged() => OnChange?.Invoke();
    }
}