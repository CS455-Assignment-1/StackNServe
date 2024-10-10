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
        private GlobalStringListService globalStringListService;
        private SelectionButtonService selectionButtonService;
        private Timer _timer;
        private Mock<HttpClient> mockHttpClientTemp;

        public ToppingsSelectComponentTests()
        {
            _mockStringListService = new Mock<GlobalStringListService>();
            _mockSelectionButtonService = new Mock<SelectionButtonService>();

            Services.AddSingleton(_mockStringListService.Object);
            Services.AddSingleton(_mockSelectionButtonService.Object);

            _mockHttpClient = new Mock<HttpClient>();
            Services.AddSingleton(_mockHttpClient.Object);

            globalStringListService = new GlobalStringListService();
            selectionButtonService = new SelectionButtonService();
            Services.AddSingleton(globalStringListService);
            Services.AddSingleton(selectionButtonService);

            mockHttpClientTemp = new Mock<HttpClient>();
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
        [Fact]
        public void ToggleMenu_ExpandsAndCollapsesMenu()
        {
            var component = RenderComponent<Toppings_Select>();

            component.Find("button.ToppingToggleButton").Click();
            Assert.True(component.Instance.isExpanded);

            component.Find("button.ToppingToggleButton").Click();
            Assert.False(component.Instance.isExpanded);
        }
        [Fact]
        public void AddToBurger_AddsToppingsToBurgerStack()
        {
            var component = RenderComponent<StackNServe.Shared.Toppings_Select>();

            component.InvokeAsync(() => component.Instance.AddToBurger("images/Toppings/Avocado.png"));

            Assert.Contains("Avocado", globalStringListService.StringList);
        }

        [Fact]
        public void ClearHoverInfo_ClearsHoverInfo()
        {
            var component = RenderComponent<Toppings_Select>();
            component.Instance.currentHoverToppings = "images/Toppings/Avocado.png";
            component.Instance.currentHoverInfo = new Toppings_Select.ToppingInfo("Avocado", "Description", 10);

            component.InvokeAsync(() => component.Instance.ClearHoverInfo());

            Assert.Null(component.Instance.currentHoverToppings);
            Assert.Null(component.Instance.currentHoverInfo);
        }
        [Fact]
        public void ToggleMenu_TogglesMenuState()
        {
            var component = RenderComponent<StackNServe.Shared.Toppings_Select>();

            component.InvokeAsync(() => component.Instance.ToggleMenu());

            Assert.True(component.Instance.isExpanded);
        }
        [Fact]
        public void CheckToppingsSelectVar_CollapsesMenuIfToppingsSelectVarIsFalse()
        {
            var component = RenderComponent<StackNServe.Shared.Toppings_Select>();
            component.InvokeAsync(() => component.Instance.ToggleMenu());
            Assert.True(component.Instance.isExpanded);

            selectionButtonService.ToppingSelectVar = false;
            component.Instance.CheckToppingsSelectVar(null);

            Assert.False(component.Instance.isExpanded);
        }
        [Fact]
        public void CheckToppingsSelectVar_DoesNotCollapseMenuIfToppingsSelectVarIsTrue()
        {
            var component = RenderComponent<StackNServe.Shared.Toppings_Select>();

            component.InvokeAsync(() => component.Instance.ToggleMenu());
            Assert.True(component.Instance.isExpanded);

            selectionButtonService.ToppingSelectVar = true;
            component.Instance.CheckToppingsSelectVar(null);

            Assert.True(component.Instance.isExpanded);
        }
    }
}