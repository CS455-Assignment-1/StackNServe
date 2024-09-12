using Xunit;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Web;
using StackNServe.Services;
using StackNServe.Pages;
using System.Threading.Tasks;
using StackNServe.Shared;
using StackNServe.Models;

namespace StackNServe.Tests
{
    public class PattySelectComponentTests : TestContext
    {
        public PattySelectComponentTests()
        {
            Services.AddSingleton<GlobalStringListService>();
            Services.AddSingleton<SelectionButtonService>();
        }

        [Fact]
        public void PattySelect_Should_Render_Correctly()
        {
            // Arrange
            var cut = RenderComponent<Patty_Select>();

            // Assert
            cut.Find("button.PattyToggleButton");
            cut.Find("img.Patty_Select_Image");
            Assert.False(cut.Instance.isExpanded);
        }

        [Fact]
        public async Task PattySelect_ToggleMenu_Should_Toggle_ExpandedState()
        {
            // Arrange
            var cut = RenderComponent<Patty_Select>();

            // Act
            var button = cut.Find("button.PattyToggleButton");
            await cut.InvokeAsync(() => button.Click());

            // Assert
            Assert.True(cut.Instance.isExpanded);
            cut.Find("ul.ClickExpandMenu");

            // Act again to close the menu
            await cut.InvokeAsync(() => button.Click());

            // Assert
            Assert.False(cut.Instance.isExpanded);
            Assert.Empty(cut.FindAll("ul.ClickExpandMenu"));
        }

        [Fact]
        public async Task Hovering_Over_All_Patties_Should_Display_Info()
        {
            // Arrange
            var cut = RenderComponent<Patty_Select>();

            for (int i = 0; i < 4; i++)
            {
                // Expand the menu first by clicking the toggle button
                var button = cut.Find("button.PattyToggleButton");
                await cut.InvokeAsync(() => button.Click());

                // Expected patty names
                var expectedPattyNames = new List<string> { "Veggie Patty", "Chicken Patty", "Fish Patty", "Portobello Mushroom Patty" };

                // Find all the <li> elements
                var pattyItemList = cut.FindAll("li");

                // Simulate the mouse enter event
                await cut.InvokeAsync(() => pattyItemList[i].TriggerEventAsync("onmouseenter", new MouseEventArgs()));

                // Assert that the hover info is displayed correctly
                Assert.NotNull(cut.Instance.currentHoverInfo);
                Assert.Equal(expectedPattyNames[i], cut.Instance.currentHoverInfo.Name); // Adjust based on actual patty data

                // Simulate the mouse leave event
                await cut.InvokeAsync(() => pattyItemList[i].TriggerEventAsync("onmouseleave", new MouseEventArgs()));

                // Assert that the hover info is cleared
                Assert.Null(cut.Instance.currentHoverInfo);

                // Close the menu before moving to the next patty
                await cut.InvokeAsync(() => button.Click());
            }
        }

        [Fact]
        public async Task PattySelect_ToggleMenu_Should_Update_SelectionButtonService_State()
        {
            // Arrange
            var selectionButtonService = new SelectionButtonService();
            Services.AddSingleton(selectionButtonService);
            var cut = RenderComponent<Patty_Select>();

            // Act
            var button = cut.Find("button.PattyToggleButton");
            await cut.InvokeAsync(() => button.Click());

            // Assert
            Assert.True(selectionButtonService.PattySelectVar);
            Assert.False(selectionButtonService.BunSelectVar);
            Assert.False(selectionButtonService.SaucesSelectVar);
            Assert.False(selectionButtonService.ToppingSelectVar);
        }

        [Fact]
        public async Task AddToBurger_Should_Add_Correct_Patty_To_StringListService()
        {
            // Arrange
            var stringListService = new GlobalStringListService();
            Services.AddSingleton(stringListService);
            var cut = RenderComponent<Patty_Select>();

            // Act
            await cut.InvokeAsync(() => cut.Instance.AddToBurger("images/Patty/Veggie_Patty.png"));

            // Assert
            Assert.Contains("Veggie Patty", stringListService.StringList);
        }

        [Fact]
        public async Task PattySelect_Should_Close_After_AutoClose_Timer()
        {
            // Arrange
            var cut = RenderComponent<Patty_Select>();

            // Act
            var button = cut.Find("button.PattyToggleButton");
            await cut.InvokeAsync(() => button.Click());

            // Simulate mouse enter and leave events on the menu
            var menu = cut.Find("ul.ClickExpandMenu");
            await cut.InvokeAsync(() => menu.TriggerEventAsync("onmouseenter", new MouseEventArgs())); // Cancels auto-close
            await cut.InvokeAsync(() => menu.TriggerEventAsync("onmouseleave", new MouseEventArgs())); // Starts auto-close

            // Wait for auto-close (manually adjusting wait time in the test to simulate timer)
            await Task.Delay(100); // Adjust as needed for testing

            // Assert
            Assert.False(cut.Instance.isExpanded); // Menu should auto-close
        }
    }
}
