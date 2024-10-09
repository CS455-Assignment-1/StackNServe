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
    public class ToppingsSelectComponentTests : TestContext
    {
        private Mock<GlobalStringListService> _mockStringListService;
        private Mock<SelectionButtonService> _mockSelectionButtonService;
        private Mock<HttpClient> _mockHttpClient;

        public ToppingsSelectComponentTests()
        {
            _mockStringListService = new Mock<GlobalStringListService>();
            _mockSelectionButtonService = new Mock<SelectionButtonService>();

            Services.AddSingleton(_mockStringListService.Object);
            Services.AddSingleton(_mockSelectionButtonService.Object);

            _mockHttpClient = new Mock<HttpClient>();
            Services.AddSingleton(_mockHttpClient.Object);
        }
        [Fact]
        public void ToppingsSelectComponent_RendersCorrectly()
        {
            var component = RenderComponent<Toppings_Select>();

            var toggleButton = component.Find("button.ToppingToggleButton");
            var toppingsIcon = component.Find("img.Topping_Select_Image");

            Assert.NotNull(toggleButton);
            Assert.NotNull(toppingsIcon);
            Assert.Equal("images/Toppings_Select.png", toppingsIcon.GetAttribute("src"));
        }
        [Fact]
        public void SelectComponent_TogglesMenuCorrectly()
        {
            var component = RenderComponent<Toppings_Select>();

            Assert.DoesNotContain("ClickExpandMenu", component.Markup);

            var toggleButton = component.Find("button.ToppingToggleButton");
            toggleButton.Click();

            Assert.Contains("ClickExpandMenu", component.Markup);
        }
        [Fact]
        public async Task ToppingsSelectComponent_HoverDisplaysInfo()
        {
            var component = RenderComponent<Toppings_Select>();
            var toggleButton = component.Find("button.ToppingToggleButton");
            toggleButton.Click();
            var toppingsItems = component.FindAll("img.ImageSmallCircular");

            Assert.NotEmpty(toppingsItems);  

            var toppingsImage = toppingsItems[0];
            await component.InvokeAsync(() => component.Instance.Display_Info("images/Toppings/Avocado.png"));

            Assert.Contains("Avocado", component.Markup);
        }
        [Fact]
        public void ToppingsSelectComponent_CollapsesMenuOnToggle()
        {
            var component = RenderComponent<Toppings_Select>();
            var toggleButton = component.Find("button.ToppingToggleButton");
            toggleButton.Click();

            Assert.Contains("ClickExpandMenu", component.Markup);

            toggleButton.Click();
            
            Assert.DoesNotContain("ClickExpandMenu", component.Markup);
        }
    }
}