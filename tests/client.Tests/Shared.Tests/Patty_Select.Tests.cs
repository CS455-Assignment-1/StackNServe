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
    public class PattySelectComponentTests : TestContext
    {
        private Mock<GlobalStringListService> _mockStringListService;
        private Mock<SelectionButtonService> _mockSelectionButtonService;
        private Mock<HttpClient> _mockHttpClient;
        private GlobalStringListService globalStringListService;
        private SelectionButtonService selectionButtonService;
        private Timer _timer;
        private Mock<HttpClient> mockHttpClientTemp;

        public PattySelectComponentTests()
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
        public void PattySelectComponent_RendersCorrectly()
        {
            var component = RenderComponent<Patty_Select>();

            var toggleButton = component.Find("button.PattyToggleButton");
            var pattyIcon = component.Find("img.Patty_Select_Image");

            Assert.NotNull(toggleButton);
            Assert.NotNull(pattyIcon);
            Assert.Equal("images/Patty_Select.png", pattyIcon.GetAttribute("src"));
        }
        [Fact]
        public void SelectComponent_TogglesMenuCorrectly()
        {
            var component = RenderComponent<Patty_Select>();

            Assert.DoesNotContain("ClickExpandMenu", component.Markup);

            var toggleButton = component.Find("button.PattyToggleButton");
            toggleButton.Click();

            Assert.Contains("ClickExpandMenu", component.Markup);
        }

        [Fact]
        public async Task PattySelectComponent_HoverDisplaysInfo()
        {
            var component = RenderComponent<Patty_Select>();

            await component.InvokeAsync(() => component.Instance.PrefetchPattyDescriptions());

            var toggleButton = component.Find("button.PattyToggleButton");
            toggleButton.Click();

            var pattyItems = component.FindAll("img.ImageSmallCircular");
            Assert.NotEmpty(pattyItems);

            var firstPattyImage = pattyItems[0];
            await component.InvokeAsync(() => component.Instance.Display_Info("images/Patty/Veggie_Patty.png"));

            component.Render(); 
            Assert.Contains("Veggie Patty", component.Markup);
        }

        [Fact]
        public void PattySelectComponent_CollapsesMenuOnToggle()
        {
            var component = RenderComponent<Patty_Select>();
            var toggleButton = component.Find("button.PattyToggleButton");
            toggleButton.Click();

            Assert.Contains("ClickExpandMenu", component.Markup);

            toggleButton.Click();
            
            Assert.DoesNotContain("ClickExpandMenu", component.Markup);
        }
        [Fact]
        public void ToggleMenu_ExpandsAndCollapsesMenu()
        {
            var component = RenderComponent<Patty_Select>();

            component.Find("button.PattyToggleButton").Click();
            Assert.True(component.Instance.isExpanded);

            component.Find("button.PattyToggleButton").Click();
            Assert.False(component.Instance.isExpanded);
        }
        [Fact]
        public void AddToBurger_AddsPattyToBurgerStack()
        {
            var component = RenderComponent<StackNServe.Shared.Patty_Select>();

            component.InvokeAsync(() => component.Instance.AddToBurger("images/Patty/Veggie_Patty.png"));

            Assert.Contains("Veggie Patty", globalStringListService.StringList);
        }

        [Fact]
        public void ClearHoverInfo_ClearsHoverInfo()
        {
            var component = RenderComponent<Patty_Select>();
            component.Instance.currentHoverPatty = "images/Patty/Veggie_Patty.png";
            component.Instance.currentHoverInfo = new Patty_Select.PattyInfo("Veggine Patty", "Description", 10);

            component.InvokeAsync(() => component.Instance.ClearHoverInfo());

            Assert.Null(component.Instance.currentHoverPatty);
            Assert.Null(component.Instance.currentHoverInfo);
        }
        [Fact]
        public void ToggleMenu_TogglesMenuState()
        {
            var component = RenderComponent<StackNServe.Shared.Patty_Select>();

            component.InvokeAsync(() => component.Instance.ToggleMenu());

            Assert.True(component.Instance.isExpanded);
        }
        [Fact]
        public void CheckPattySelectVar_CollapsesMenuIfPattySelectVarIsFalse()
        {
            var component = RenderComponent<StackNServe.Shared.Patty_Select>();
            component.InvokeAsync(() => component.Instance.ToggleMenu());
            Assert.True(component.Instance.isExpanded);

            selectionButtonService.PattySelectVar = false;
            component.Instance.CheckPattySelectVar(null);

            Assert.False(component.Instance.isExpanded);
        }
        [Fact]
        public void CheckPattySelectVar_DoesNotCollapseMenuIfPattySelectVarIsTrue()
        {
            var component = RenderComponent<StackNServe.Shared.Patty_Select>();

            component.InvokeAsync(() => component.Instance.ToggleMenu());
            Assert.True(component.Instance.isExpanded);

            selectionButtonService.PattySelectVar = true;
            component.Instance.CheckPattySelectVar(null);

            Assert.True(component.Instance.isExpanded);
        }
    }
}