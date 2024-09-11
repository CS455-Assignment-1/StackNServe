using Xunit;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using StackNServe.Services;
using StackNServe.Pages;
using System.Threading.Tasks;
using StackNServe.Shared;
using StackNServe.Models;

namespace StackNServe.Tests
{
    public class BunSelectComponentTests : TestContext
    {
        public BunSelectComponentTests()
        {
            Services.AddSingleton<GlobalStringListService>();
            Services.AddSingleton<SelectionButtonService>();
        }

        [Fact]
        public void BunSelect_Should_Render_Correctly()
        {
            // Arrange
            Services.AddSingleton<SelectionButtonService>();

            // Act
            var cut = RenderComponent<Bun_Select>();

            // Assert
            cut.Find("button.BunToggleButton");
            cut.Find("img.Bun_Select_Image");
            Assert.False(cut.Instance.isExpanded);
        }

        [Fact]
        public async Task BunSelect_ToggleMenu_Should_Toggle_ExpandedState()
        {
            // Arrange
            Services.AddSingleton<SelectionButtonService>();
            var cut = RenderComponent<Bun_Select>();

            // Act
            var button = cut.Find("button.BunToggleButton");
            await cut.InvokeAsync(() => button.Click());

            // Assert
            Assert.True(cut.Instance.isExpanded);
            cut.Find("ul.ClickExpandMenu");

            // Act 
            await cut.InvokeAsync(() => button.Click());

            // Assert
            Assert.False(cut.Instance.isExpanded);
            Assert.Empty(cut.FindAll("ul.ClickExpandMenu"));
        }

        [Fact]
        public async Task BunSelect_ToggleMenu_Should_Update_SelectionButtonService_State()
        {
            // Arrange
            var selectionButtonService = new SelectionButtonService();
            Services.AddSingleton<SelectionButtonService>(selectionButtonService);
            var cut = RenderComponent<Bun_Select>();

            // Act
            var button = cut.Find("button.BunToggleButton");
            await cut.InvokeAsync(() => button.Click());

            // Assert
            Assert.True(selectionButtonService.BunSelectVar);
            Assert.False(selectionButtonService.PattySelectVar);
            Assert.False(selectionButtonService.SaucesSelectVar);
            Assert.False(selectionButtonService.ToppingSelectVar);
        }

        [Fact]
        public void AddToBurger_Should_Add_Correct_Bun_To_StringListService()
        {
            // Arrange
            var stringListService = new GlobalStringListService();
            Services.AddSingleton(stringListService);
            var selectionButtonService = new SelectionButtonService();
            Services.AddSingleton(selectionButtonService);

            var cut = RenderComponent<Bun_Select>();

            // Act
            cut.InvokeAsync(() => cut.Instance.AddToBurger("images/Bun/Garlic_Bun.png"));

            // Assert
            Assert.Contains("Bun Bottom", stringListService.StringList);
        }
    }
}
