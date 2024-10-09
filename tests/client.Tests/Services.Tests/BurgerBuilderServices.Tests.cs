using Bunit;
using Xunit;
using StackNServe.Pages;
using StackNServe.Services;
using StackNServe.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using Microsoft.JSInterop;
using Moq;
using Moq.Protected;
using System.Net.Http;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;

namespace StackNServe.Tests
{
    public class BurgerServiceTests : TestContext
    {
        [Fact]
        public void AddComponent_AddsComponentToBurgerStack()
        {
            var burgerService = new BurgerService();
            var component = new BurgerComponent { Name = "Lettuce", Color = "Green" };

            burgerService.AddComponent(component);

            Assert.Single(burgerService.BurgerStack);
            Assert.Equal("Lettuce", burgerService.BurgerStack[0].Name);
            Assert.Equal("Green", burgerService.BurgerStack[0].Color);
        }

        [Fact]
        public void ClearBurger_ClearsAllComponents()
        {
            var burgerService = new BurgerService();
            
            burgerService.AddComponent(new BurgerComponent { Name = "Lettuce", Color = "Green" });
            burgerService.AddComponent(new BurgerComponent { Name = "Tomato", Color = "Red" });
            burgerService.ClearBurger();

            Assert.Empty(burgerService.BurgerStack);
        }

        [Fact]
        public void AddComponent_InvokesOnBurgerChangedEvent()
        {
            var burgerService = new BurgerService();
            var eventInvoked = false;

            burgerService.OnBurgerChanged += () => eventInvoked = true;
            burgerService.AddComponent(new BurgerComponent { Name = "Cheese", Color = "Yellow" });

            Assert.True(eventInvoked);
        }

        [Fact]
        public void ClearBurger_InvokesOnBurgerChangedEvent()
        {
            var burgerService = new BurgerService();
            var eventInvoked = false;

            burgerService.OnBurgerChanged += () => eventInvoked = true;
            burgerService.AddComponent(new BurgerComponent { Name = "Lettuce", Color = "Green" });
            burgerService.ClearBurger();

            Assert.True(eventInvoked);
        }

        [Fact]
        public void BurgerStack_ReturnsReadOnlyList()
        {
            var burgerService = new BurgerService();
            var burgerStack = burgerService.BurgerStack;

            Assert.IsAssignableFrom<IReadOnlyList<BurgerComponent>>(burgerStack);
        }
    }
}
