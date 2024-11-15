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
    public class BunSelectComponentTests : TestContext
    {
        private Mock<HttpMessageHandler> mockHttpMessageHandler;
        private HttpClient mockHttpClient;
        private Mock<GlobalStringListService> mockStringListService;
        private Mock<SelectionButtonService> mockSelectionButtonService;
        private GlobalStringListService globalStringListService;
        private SelectionButtonService selectionButtonService;
        private Timer _timer;
        private Mock<HttpClient> mockHttpClientTemp;

        public BunSelectComponentTests()
        {
            mockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHttpClient = new HttpClient(mockHttpMessageHandler.Object);
            mockStringListService = new Mock<GlobalStringListService>();
            mockSelectionButtonService = new Mock<SelectionButtonService>();
            mockHttpClientTemp = new Mock<HttpClient>();

            Services.AddSingleton(mockHttpClient);
            Services.AddSingleton(mockStringListService.Object);
            Services.AddSingleton(mockSelectionButtonService.Object);

            globalStringListService = new GlobalStringListService();
            selectionButtonService = new SelectionButtonService();
            Services.AddSingleton(globalStringListService);
            Services.AddSingleton(selectionButtonService);
        }
        [Fact]
        public void BunSelectComponent_RendersCorrectly()
        {
            var component = RenderComponent<Bun_Select>();

            var toggleButton = component.Find("button.BunToggleButton");
            var bunIcon = component.Find("img.Bun_Select_Image");

            Assert.NotNull(toggleButton);
            Assert.NotNull(bunIcon);
            Assert.Equal("images/Bun_Select.png", bunIcon.GetAttribute("src"));
        }
        [Fact]
        public void BunSelectComponent_TogglesMenuCorrectly()
        {
            var component = RenderComponent<Bun_Select>();

            Assert.DoesNotContain("ClickExpandMenu", component.Markup);

            var toggleButton = component.Find("button.BunToggleButton");
            toggleButton.Click();

            Assert.Contains("ClickExpandMenu", component.Markup);
        }
        
        [Fact]
        public async Task BunSelectComponent_HoverDisplaysInfo()
        {
            var component = RenderComponent<Bun_Select>();

            var toggleButton = component.Find("button.BunToggleButton");
            toggleButton.Click();

            var bunItems = component.FindAll("img.ImageSmallCircular");
            Assert.NotEmpty(bunItems);

            var firstBunImage = bunItems[0];
            await component.InvokeAsync(() => component.Instance.Display_Info("images/Bun/Garlic_Bun.png"));


            component.Render(); 
            Assert.Contains("Garlic Bun", component.Markup);
        }

        [Fact]
        public void BunSelectComponent_CollapsesMenuOnToggle()
        {
            var component = RenderComponent<Bun_Select>();
            var toggleButton = component.Find("button.BunToggleButton");
            toggleButton.Click();

            Assert.Contains("ClickExpandMenu", component.Markup);

            toggleButton.Click();
            
            Assert.DoesNotContain("ClickExpandMenu", component.Markup);
        }
        [Fact]
        public void ToggleMenu_ExpandsAndCollapsesMenu()
        {
            var component = RenderComponent<Bun_Select>();

            component.Find("button.BunToggleButton").Click();
            Assert.True(component.Instance.isExpanded);

            component.Find("button.BunToggleButton").Click();
            Assert.False(component.Instance.isExpanded);
        }
        [Fact]
        public void AddToBurger_AddsBunToBurgerStack()
        {
            var component = RenderComponent<StackNServe.Shared.Bun_Select>();

            component.InvokeAsync(() => component.Instance.AddToBurger("images/Bun/Plain_Bun.png"));
            component.InvokeAsync(() => component.Instance.AddToBurger("images/Bun/Plain_Bun.png"));

            Assert.Contains("Bun Bottom", globalStringListService.StringList);
            Assert.Contains("Plain Bun", globalStringListService.StringList);
        }

        [Fact]
        public void AddToBurger_FirstBunIsBottomBun()
        {
            var component = RenderComponent<StackNServe.Shared.Bun_Select>();

            component.InvokeAsync(() => component.Instance.AddToBurger("images/Bun/Plain_Bun.png"));

            Assert.Contains("Bun Bottom", globalStringListService.StringList);
        }

        [Fact]
        public void AddToBurger_SecondBunIsTopBun()
        {
            globalStringListService.AddString("Bun Bottom"); // Simulate existing bottom bun
            var component = RenderComponent<StackNServe.Shared.Bun_Select>();

            component.InvokeAsync(() => component.Instance.AddToBurger("images/Bun/Plain_Bun.png"));

            Assert.Contains("Plain Bun", globalStringListService.StringList);
            Assert.DoesNotContain("Bun Bottom", globalStringListService.StringList.Skip(1));
        }
        [Fact]
        public void AddToBurger_AddsTopBunWhenBottomExists()
        {
            globalStringListService.AddString("Bun Bottom"); // Simulate an existing bottom bun
            var component = RenderComponent<StackNServe.Shared.Bun_Select>();

            component.InvokeAsync(() => component.Instance.AddToBurger("images/Bun/Sesame_Bun.png"));

            Assert.Contains("Sesame Bun", globalStringListService.StringList);
        }
        [Fact]
        public void ClearHoverInfo_ClearsHoverInfo()
        {
            var component = RenderComponent<Bun_Select>();
            component.Instance.currentHoverBun = "images/Bun/Plain_Bun.png";
            component.Instance.currentHoverInfo = new Bun_Select.BunInfo("Plain Bun", "Description", 10);

            component.InvokeAsync(() => component.Instance.ClearHoverInfo());

            Assert.Null(component.Instance.currentHoverBun);
            Assert.Null(component.Instance.currentHoverInfo);
        }
        [Fact]
        public void ToggleMenu_TogglesMenuState()
        {
            var component = RenderComponent<StackNServe.Shared.Bun_Select>();

            component.InvokeAsync(() => component.Instance.ToggleMenu());

            Assert.True(component.Instance.isExpanded);
        }
        [Fact]
        public void CheckBunSelectVar_CollapsesMenuIfBunSelectVarIsFalse()
        {
            var component = RenderComponent<StackNServe.Shared.Bun_Select>();
            component.InvokeAsync(() => component.Instance.ToggleMenu());
            Assert.True(component.Instance.isExpanded);

            selectionButtonService.BunSelectVar = false;
            component.Instance.CheckBunSelectVar(null);

            Assert.False(component.Instance.isExpanded);
        }
        [Fact]
        public void CheckBunSelectVar_DoesNotCollapseMenuIfBunSelectVarIsTrue()
        {
            var component = RenderComponent<StackNServe.Shared.Bun_Select>();

            component.InvokeAsync(() => component.Instance.ToggleMenu());
            Assert.True(component.Instance.isExpanded);

            selectionButtonService.BunSelectVar = true;
            component.Instance.CheckBunSelectVar(null);

            Assert.True(component.Instance.isExpanded);
        }


    }
}