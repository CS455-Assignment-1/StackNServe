using Xunit;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using StackNServe.Pages;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace StackNServe.Tests
{
    public class PlayAgainComponentTests : TestContext
    {
        [Fact]
        public void PlayAgain_Should_Render_Correctly()
        {
            // Arrange & Act
            var cut = RenderComponent<Play_Again>();

            // Assert: Verify the heading and button rendering
            var heading = cut.Find("h1");
            Assert.Equal("PLAY", heading.TextContent);
        }

        [Fact]
        public void PlayAgainButton_Should_Navigate_To_New_Game()
        {
            // Arrange
            var navManager = Services.GetRequiredService<NavigationManager>() as Bunit.TestDoubles.FakeNavigationManager;
            var cut = RenderComponent<Play_Again>();

            // Act: Simulate a click on the Play Again button
            var playAgainButton = cut.Find("div.Play_Again");
            playAgainButton.Click();

            // Assert: Verify navigation to "New_Game"
            Assert.NotNull(navManager);
            Assert.Equal("New_Game", navManager?.Uri.Replace(navManager.BaseUri, string.Empty));
        }
    }
}

