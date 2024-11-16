using Bunit;
using Xunit;
using Moq;
using Moq.Protected;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using StackNServe.Pages;
using StackNServe.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;
using System.Threading;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace StackNServe.Tests
{
    public class HomeTests : TestContext
    {
        private HttpClient mockHttpClient;
        private Mock<GlobalStringListService> mockStringListService;
        private Mock<IJSRuntime> mockJSRuntime;
        private Mock<HttpMessageHandler> mockHttpMessageHandler;

        public HomeTests()
        {
            mockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Loose);
            mockHttpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://stacknserve.onrender.com")
            };
            mockStringListService = new Mock<GlobalStringListService>();
            mockJSRuntime = new Mock<IJSRuntime>();

            Services.AddSingleton(mockHttpClient);
            Services.AddSingleton(mockStringListService.Object);
            Services.AddSingleton(mockJSRuntime.Object);

            Services.AddSingleton<SelectionButtonService>();
        }

        [Fact]
        public void NewGamePage_PageRendersMainGame()
        {
            var component = RenderComponent<Home>(parameters => parameters
                .Add(p => p.isGameStarting, false)
                .Add(p => p.isEnded, false).Add(p => p.IsTestMode, true));
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
                .Add(p => p.isGameStarting, true).Add(p => p.IsTestMode, true));
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
                .Add(p => p.isEnded, true).Add(p => p.IsTestMode, true));

            var timeFinishedMessage = component.Find("h1.Time_Elapsed_Message");
            var scoreMessage = component.Find("h2.Time_Elapsed_Score");
            var playAgainButton = component.Find("div.Play_Again_Button");

            Assert.NotNull(timeFinishedMessage);
            Assert.NotNull(scoreMessage);
            Assert.NotNull(playAgainButton);
        }
           

        [Fact]
        public async Task ValidateAndStartGame_InvalidPlayerName_ShowsError()
        {
            var component = RenderComponent<Home>(parameters => parameters
    .Add(p => p.IsTestMode, true));

            component.Instance.playerName = "Invalid@Name";

            await component.Instance.validate_and_start_game();

            Assert.True(component.Instance.is_input_valid);
            Assert.Equal("Name can only contain letters and numbers!", component.Instance.error_message);
        } 


        [Fact]
        public async Task FetchPlayerScore_UpdatesPlayerScore()
        {
            var component = RenderComponent<Home>(parameters => parameters
    .Add(p => p.IsTestMode, true));
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("100")
            };

            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(mockResponse);

            await component.InvokeAsync(() => component.Instance.fetch_player_score());

            Assert.Equal(100, component.Instance.current_player_score); 
        }

        [Fact]
        public async Task PlayerSkip_ReducesScoreBy10()
        {
            var mockOrderListResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("\"Bun Bottom,Plain Bun,Chicken Patty\"")
            };
            var mockOrderPriceResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("50")
            };
            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString().Contains("orderList")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(mockOrderListResponse);

            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString().Contains("orderPrice")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(mockOrderPriceResponse);

            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri.ToString().Contains("updateScore")),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            var component = RenderComponent<Home>(parameters => parameters
    .Add(p => p.IsTestMode, true));
            await component.InvokeAsync(() => component.Instance.player_skip());

            Assert.Equal(-10, component.Instance.current_player_score);
        }
        [Fact]
        public async Task FetchPlayerScore_SetsCurrentPlayerScore()
        {
            var component = RenderComponent<Home>(parameters => parameters
    .Add(p => p.IsTestMode, true));
            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync", 
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri.ToString().Contains("fetchScore")),
                    ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("150")
                });

            await component.InvokeAsync(() => component.Instance.fetch_player_score());

            Assert.Equal(150, component.Instance.current_player_score);
        }

        [Fact]
        public async Task FetchOrderPrice_SetsCurrentOrderPrice()
        {
            var component = RenderComponent<Home>(parameters => parameters
    .Add(p => p.IsTestMode, true));

            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString().Contains("orderPrice")),
                    ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("100")
                });

            await component.InvokeAsync(() => component.Instance.fetch_order_price());

            Assert.Equal(100, component.Instance.current_order_price);
        }

        [Fact]
        public async Task FetchOrderList_SetsOrderList()
        {
            var component = RenderComponent<Home>(parameters => parameters
    .Add(p => p.IsTestMode, true));
            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString().Contains("orderList")),
                    ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("[\"Bun Bottom\", \"Chicken Patty\", \"Plain Bun\"]")
                });

            await component.InvokeAsync(() => component.Instance.fetch_order_list());

            Assert.Equal(new List<string> { "Bun Bottom", "Chicken Patty", "Plain Bun" }, component.Instance.current_order_list);
        }

        [Fact]
        public async Task CreatePlayer_SetsPlayerId()
        {
            var component = RenderComponent<Home>(parameters => parameters
    .Add(p => p.IsTestMode, true));

            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri.ToString().Contains("createPlayer")),
                    ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"player_id\": 5}")
                });

            await component.InvokeAsync(() => component.Instance.create_player());

            Assert.Equal(5, component.Instance.current_player_id);  // Player ID should be set
        }
        [Fact]
        public async Task CheckList_UpdatesScoreAndMessage()
        {
            var component = RenderComponent<Home>(parameters => parameters
    .Add(p => p.IsTestMode, true));

            var orderCheckResponse = new OrderCheckResponse
            {
                Message = "Correct order!",
                FinalScore = 120
            };

            var mockOrderListResponse = "[\"Bun Bottom\",\"Chicken Patty\",\"Plain Bun\"]";
            var mockOrderPriceResponse = "50";

            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString().Contains("orderList")),
                    ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(mockOrderListResponse)
                });

            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString().Contains("orderPrice")),
                    ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(mockOrderPriceResponse)
                });

            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri.ToString().Contains("checkList")),
                    ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = JsonContent.Create(orderCheckResponse)
                });

            await component.InvokeAsync(() => component.Instance.CheckList());

            Assert.Equal("Correct order!", component.Instance.message);
            Assert.Equal(120, component.Instance.current_player_score);
        }

        [Fact]
        public async Task CheckList_ClearsStringListAndUpdatesOrder()
        {
            var component = RenderComponent<Home>(parameters => parameters
    .Add(p => p.IsTestMode, true));

            var orderCheckResponse = new OrderCheckResponse
            {
                Message = "Correct order!",
                FinalScore = 100
            };

            var mockOrderListResponse = "[\"Bun Bottom\",\"Chicken Patty\",\"Plain Bun\"]";
            var mockOrderPriceResponse = "50";

            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString().Contains("orderList")),
                    ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(mockOrderListResponse)
                });

            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString().Contains("orderPrice")),
                    ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(mockOrderPriceResponse)
                });

            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri.ToString().Contains("checkList")),
                    ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = JsonContent.Create(orderCheckResponse)
                });

            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri.ToString().Contains("updateScore")),
                    ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            mockStringListService.Setup(s => s.ClearList()).Verifiable();

            await component.InvokeAsync(() => component.Instance.CheckList());

            mockStringListService.Verify(s => s.ClearList(), Times.Once);
            Assert.Equal("Correct order!", component.Instance.message);
            Assert.Equal(100, component.Instance.current_player_score);
        }

        [Fact]
        public async Task HandleTimerFinished_SetsGameEnded()
        {
            var component = RenderComponent<Home>(parameters => parameters
    .Add(p => p.IsTestMode, true));

            component.Instance.handle_timer_finished();

            Assert.True(component.Instance.isEnded);
        }

        [Fact]
        public async Task FetchPlayerScoreName_ReturnsCorrectScore()
        {
            var component = RenderComponent<Home>(parameters => parameters
    .Add(p => p.IsTestMode, true));
            
            var playerName = "ValidPlayer";
            var expectedScore = 150;
            
            var postData = new Dictionary<string, string> { { "player_name", playerName } };
            var mockResponseContent = expectedScore.ToString();
            var mockResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(mockResponseContent)
            };

            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Post &&
                        req.RequestUri.ToString().Contains("fetchScoreName")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockResponseMessage);

            var result = await component.Instance.fetch_player_score_name(playerName);

            Assert.Equal(expectedScore, result);
        }

    }
}