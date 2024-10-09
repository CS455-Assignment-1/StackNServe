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

        public PattySelectComponentTests()
        {
            _mockStringListService = new Mock<GlobalStringListService>();
            _mockSelectionButtonService = new Mock<SelectionButtonService>();

            Services.AddSingleton(_mockStringListService.Object);
            Services.AddSingleton(_mockSelectionButtonService.Object);

            _mockHttpClient = new Mock<HttpClient>();
            Services.AddSingleton(_mockHttpClient.Object);
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
            var toggleButton = component.Find("button.PattyToggleButton");
            toggleButton.Click();
            var pattyItems = component.FindAll("img.ImageSmallCircular");

            Assert.NotEmpty(pattyItems);  

            var pattyImage = pattyItems[0];
            await component.InvokeAsync(() => component.Instance.Display_Info("images/Patty/Veggie_Patty.png"));

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
    }
}