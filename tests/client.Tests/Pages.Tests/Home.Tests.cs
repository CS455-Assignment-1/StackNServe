using Bunit;
using Xunit;
using StackNServe.Pages;
using StackNServe.Services;
using StackNServe.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using Microsoft.JSInterop;
using Moq;
using System.Net.Http;

namespace StackNServe.Tests
{
    public class HomeTests : TestContext
    {
        private Mock<HttpClient> mockHttpClient;
        private Mock<GlobalStringListService> mockStringListService;
        private Mock<IJSRuntime> mockJSRuntime;

        public HomeTests()
        {
            mockHttpClient = new Mock<HttpClient>();
            mockStringListService = new Mock<GlobalStringListService>();
            mockJSRuntime = new Mock<IJSRuntime>();

            Services.AddSingleton(mockHttpClient.Object);
            Services.AddSingleton(mockStringListService.Object);
            Services.AddSingleton(mockJSRuntime.Object);

            Services.AddSingleton<SelectionButtonService>();
        }

        [Fact]
        public void NewGamePage_PageRendersMainGame()
        {
            var component = RenderComponent<Home>(parameters => parameters
                .Add(p => p.isGameStarting, false)
                .Add(p => p.isEnded, false)
            );
            component.FindComponent<Bun_Select>(); 
            component.FindComponent<Patty_Select>(); 
            component.FindComponent<Toppings_Select>(); 
            component.FindComponent<Sauces_Select>();
            component.FindComponent<Cooking_Table>(); 
            component.FindComponent<Order>(); 
            component.FindComponent<Skip>(); 
            component.FindComponent<Submit>();
            component.FindComponent<Score_Board>();
        }

        [Fact]
        public void NewGamePage_PageRendersStart()
        {
            var component = RenderComponent<Home>(parameters => parameters
                .Add(p => p.isGameStarting, true)
            );
            var playerNameField = component.Find("input.Player_Name_Field");
            var startGameButton = component.Find("button.Player_Name_Button");
            Assert.NotNull(playerNameField);
            Assert.NotNull(startGameButton);
        }

        [Fact]
        public void NewGamePage_PageRendersEnd()
        {
            var component = RenderComponent<Home>(parameters => parameters
                .Add(p => p.isGameStarting, false)
                .Add(p => p.isEnded, true)
            );

            var timeFinishedMessage = component.Find("h1.Time_Elapsed_Message");
            var scoreMessage = component.Find("h2.Time_Elapsed_Score");
            var playAgainButton = component.Find("div.Play_Again_Button");

            Assert.NotNull(timeFinishedMessage);
            Assert.NotNull(scoreMessage);
            Assert.NotNull(playAgainButton);
        }

    }
}
