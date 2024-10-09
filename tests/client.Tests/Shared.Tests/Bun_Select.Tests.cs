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
        private Mock<GlobalStringListService> _mockStringListService;
        private Mock<SelectionButtonService> _mockSelectionButtonService;
        private Mock<HttpClient> _mockHttpClient;

        public BunSelectComponentTests()
        {
            _mockStringListService = new Mock<GlobalStringListService>();
            _mockSelectionButtonService = new Mock<SelectionButtonService>();

            Services.AddSingleton(_mockStringListService.Object);
            Services.AddSingleton(_mockSelectionButtonService.Object);

            _mockHttpClient = new Mock<HttpClient>();
            Services.AddSingleton(_mockHttpClient.Object);
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

            var bunImage = bunItems[0];
            await component.InvokeAsync(() => component.Instance.Display_Info("images/Bun/Garlic_Bun.png"));

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
    }
}