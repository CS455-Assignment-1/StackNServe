using System;
using System.Collections.Generic;

public class BurgerComponent
{
    public string Name { get; set; }
    public string Color { get; set; }
}

public class BurgerService
{
    private List<BurgerComponent> _burgerStack = new List<BurgerComponent>();
    public event Action OnBurgerChanged;

    public IReadOnlyList<BurgerComponent> BurgerStack => _burgerStack.AsReadOnly();

    public void AddComponent(BurgerComponent component)
    {
        _burgerStack.Add(component);
        NotifyBurgerChanged();
    }

    public void ClearBurger()
    {
        _burgerStack.Clear();
        NotifyBurgerChanged();
    }

    private void NotifyBurgerChanged() => OnBurgerChanged?.Invoke();
}