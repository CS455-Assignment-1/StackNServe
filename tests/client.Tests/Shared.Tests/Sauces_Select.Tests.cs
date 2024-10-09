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
    public class SaucesSelectComponentTests : TestContext
    {
        private Mock<GlobalStringListService> _mockStringListService;
        private Mock<SelectionButtonService> _mockSelectionButtonService;
        private Mock<HttpClient> _mockHttpClient;

        public SaucesSelectComponentTests()
        {
            _mockStringListService = new Mock<GlobalStringListService>();
            _mockSelectionButtonService = new Mock<SelectionButtonService>();

            Services.AddSingleton(_mockStringListService.Object);
            Services.AddSingleton(_mockSelectionButtonService.Object);

            _mockHttpClient = new Mock<HttpClient>();
            Services.AddSingleton(_mockHttpClient.Object);
        }
        [Fact]
        public void SaucesSelectComponent_RendersCorrectly()
        {
            var component = RenderComponent<Sauces_Select>();

            var toggleButton = component.Find("button.SaucesToggleButton");
            var saucesIcon = component.Find("img.Sauces_Select_Image");

            Assert.NotNull(toggleButton);
            Assert.NotNull(saucesIcon);
            Assert.Equal("images/Sauces_Select.png", saucesIcon.GetAttribute("src"));
        }
        [Fact]
        public void SelectComponent_TogglesMenuCorrectly()
        {
            var component = RenderComponent<Sauces_Select>();

            Assert.DoesNotContain("ClickExpandMenu", component.Markup);

            var toggleButton = component.Find("button.SaucesToggleButton");
            toggleButton.Click();

            Assert.Contains("ClickExpandMenu", component.Markup);
        }
        [Fact]
        public async Task SaucesSelectComponent_HoverDisplaysInfo()
        {
            var component = RenderComponent<Sauces_Select>();
            var toggleButton = component.Find("button.SaucesToggleButton");
            toggleButton.Click();
            var saucesItems = component.FindAll("img.ImageSmallCircular");

            Assert.NotEmpty(saucesItems);  

            var saucesImage = saucesItems[0];
            await component.InvokeAsync(() => component.Instance.Display_Info("images/Sauces/Aioli.png"));

            Assert.Contains("Aioli", component.Markup);
        }
        [Fact]
        public void SaucesSelectComponent_CollapsesMenuOnToggle()
        {
            var component = RenderComponent<Sauces_Select>();
            var toggleButton = component.Find("button.SaucesToggleButton");
            toggleButton.Click();

            Assert.Contains("ClickExpandMenu", component.Markup);

            toggleButton.Click();
            
            Assert.DoesNotContain("ClickExpandMenu", component.Markup);
        }
    }
}