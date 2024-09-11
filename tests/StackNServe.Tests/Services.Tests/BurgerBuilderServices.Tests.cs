using System;
using System.Collections.Generic;
using StackNServe.Services;
using Xunit;

namespace StackNServe.Tests
{
    public class BurgerServiceTests
    {
        [Fact]
        public void AddComponent_ShouldAddComponentToBurgerStack()
        {
            // Arrange
            var burgerService = new BurgerService();
            var component = new BurgerComponent { Name = "Lettuce", Color = "Green" };

            // Act
            burgerService.AddComponent(component);

            // Assert
            Assert.Single(burgerService.BurgerStack);
            Assert.Equal("Lettuce", burgerService.BurgerStack[0].Name);
            Assert.Equal("Green", burgerService.BurgerStack[0].Color);
        }

        [Fact]
        public void ClearBurger_ShouldRemoveAllComponentsFromBurgerStack()
        {
            // Arrange
            var burgerService = new BurgerService();
            burgerService.AddComponent(new BurgerComponent { Name = "Lettuce", Color = "Green" });
            burgerService.AddComponent(new BurgerComponent { Name = "Tomato", Color = "Red" });

            // Act
            burgerService.ClearBurger();

            // Assert
            Assert.Empty(burgerService.BurgerStack);
        }

        [Fact]
        public void AddComponent_ShouldTriggerOnBurgerChangedEvent()
        {
            // Arrange
            var burgerService = new BurgerService();
            var component = new BurgerComponent { Name = "Lettuce", Color = "Green" };
            bool eventTriggered = false;

            burgerService.OnBurgerChanged += () => eventTriggered = true;

            // Act
            burgerService.AddComponent(component);

            // Assert
            Assert.True(eventTriggered);
        }

        [Fact]
        public void ClearBurger_ShouldTriggerOnBurgerChangedEvent()
        {
            // Arrange
            var burgerService = new BurgerService();
            burgerService.AddComponent(new BurgerComponent { Name = "Lettuce", Color = "Green" });
            bool eventTriggered = false;

            burgerService.OnBurgerChanged += () => eventTriggered = true;

            // Act
            burgerService.ClearBurger();

            // Assert
            Assert.True(eventTriggered);
        }

        [Fact]
        public void BurgerStack_ShouldBeReadOnly()
        {
            // Arrange
            var burgerService = new BurgerService();

            // Act
            var burgerStack = burgerService.BurgerStack;

            // Assert
            Assert.IsAssignableFrom<IReadOnlyList<BurgerComponent>>(burgerStack);
        }

        [Fact]
        public void BurgerStack_ShouldStartEmpty()
        {
            // Arrange
            var burgerService = new BurgerService();

            // Act
            var burgerStack = burgerService.BurgerStack;

            // Assert
            Assert.Empty(burgerStack);
        }
    }

    public class BurgerComponentTests
    {
        [Fact]
        public void BurgerComponent_ShouldStorePropertiesCorrectly()
        {
            // Arrange
            var component = new BurgerComponent { Name = "Patty", Color = "Brown" };

            // Assert
            Assert.Equal("Patty", component.Name);
            Assert.Equal("Brown", component.Color);
        }

        [Fact]
        public void BurgerComponent_ShouldAllowEmptyNameAndColor()
        {
            // Arrange
            var component = new BurgerComponent { Name = "", Color = "" };

            // Assert
            Assert.Equal("", component.Name);
            Assert.Equal("", component.Color);
        }
    }
}
