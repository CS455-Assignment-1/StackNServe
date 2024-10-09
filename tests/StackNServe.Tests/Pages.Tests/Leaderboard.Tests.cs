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
    public class LeaderboardTests : TestContext
    {
        public HttpClient mockHttpClient;
        public Mock<HttpMessageHandler> mockHttpMessageHandler;
        public Mock<GlobalStringListService> mockStringListService;
        public LeaderboardTests()
        {
            mockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Loose);
            mockHttpClient = new HttpClient(mockHttpMessageHandler.Object);
            mockStringListService = new Mock<GlobalStringListService>();
            Services.AddSingleton(mockHttpClient);
            Services.AddSingleton(mockStringListService.Object);
            Services.AddSingleton<SelectionButtonService>();

            var mockLeaderboardData = new List<Leaderboard.Player>
            {
                new Leaderboard.Player { name = "Player1", score = 100 },
                new Leaderboard.Player { name = "Player2", score = 90 }
            };

            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(mockLeaderboardData))
            };
            
            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()  )
            .ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(mockLeaderboardData))  
            });
        }

        [Fact]
        public void LeaderboardPage_PageRenders()
        {
            var component = RenderComponent<Leaderboard>();
            var leaderboardHeaderImage = component.Find("img.Leaderboard_Header_Image");
            var leaderboardTable = component.Find("div.Leaderboard_Table");
            var leaderboardTableHeader = component.Find("div.Leaderboard_Table_Header");
            var leaderboardTableData = component.Find("div.Leaderboard_Table_Data");

            Assert.NotNull(leaderboardHeaderImage);
            Assert.NotNull(leaderboardTable);
            Assert.NotNull(leaderboardTableHeader);
            Assert.NotNull(leaderboardTableData);
        }

        [Fact]
        public async Task LeaderboardPage_SampleScores_Displayed()
        {
            var component = RenderComponent<Leaderboard>();

            await component.InvokeAsync(() => component.Instance.load_leaderboard());

            var playerNameElements = component.FindAll(".Leaderboard_Table_Data_Name");
            var playerScoreElements = component.FindAll(".Leaderboard_Table_Data_Score");

            Assert.Equal("Player1", playerNameElements[0].TextContent);
            Assert.Equal("Player2", playerNameElements[1].TextContent);
            Assert.Equal("100", playerScoreElements[0].TextContent);
            Assert.Equal("90", playerScoreElements[1].TextContent);
        }
    }
}