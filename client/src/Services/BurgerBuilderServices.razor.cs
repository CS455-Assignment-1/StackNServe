using System;
using System.Collections.Generic;

namespace StackNServe.Services
{
    public class BurgerComponent
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }

    public class BurgerService
    {
        private readonly List<BurgerComponent> burgerStack = new List<BurgerComponent>();
        public event Action OnBurgerChanged;

        public IReadOnlyList<BurgerComponent> BurgerStack => burgerStack.AsReadOnly();

        public void AddComponent(BurgerComponent component)
        {
            burgerStack.Add(component);
            NotifyBurgerChanged();
        }

        public void ClearBurger()
        {
            burgerStack.Clear();
            NotifyBurgerChanged();
        }

        private void NotifyBurgerChanged() => OnBurgerChanged?.Invoke();
    }
}
