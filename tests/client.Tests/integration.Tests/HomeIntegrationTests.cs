using Xunit;
using Bunit;
using StackNServe.Pages;
using StackNServe.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StackNServe.Tests
{
    public class HomeIntegrationTests : TestContext
    {
        private HttpClient realHttpClient;
        private GlobalStringListService stringListService;

        public HomeIntegrationTests()
        {
            // realHttpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8000") };
            realHttpClient = new HttpClient{ BaseAddress = new System.Uri("https://stacknserve.onrender.com") };
            stringListService = new GlobalStringListService();
            Services.AddSingleton(realHttpClient);
            Services.AddSingleton(stringListService);
            Services.AddSingleton<SelectionButtonService>();
        }

        public static async Task<bool> WaitForConditionAsync(Func<Task<bool>> condition, TimeSpan timeout)
        {
            var startTime = DateTime.Now;
            while (DateTime.Now - startTime < timeout)
            {
                if (await condition())
                {
                    return true;
                }

                // Wait before checking the condition again
                await Task.Delay(100);
            }

            return false;
        }


        [Fact]
        public void StartGame_ShouldShowErrorForEmptyPlayerName()
        {
            JSInterop.SetupVoid("initializeNotification");

            var component = RenderComponent<Home>(parameters => parameters.Add(p => p.isGameStarting, true));

            // Leave player name empty
            var playerNameField = component.Find("input.Player_Name_Field");
            playerNameField.Change("");

            var startGameButton = component.Find("button.Player_Name_Button");
            startGameButton.Click();

            var errorMessage = component.Find(".error-message");
            Assert.Equal("Name cannot be empty!", errorMessage.TextContent.Trim());
        }

        [Fact]
        public void StartGame_ShouldShowErrorForInvalidPlayerName()
        {
            JSInterop.SetupVoid("initializeNotification");
            var component = RenderComponent<Home>(parameters => parameters.Add(p => p.isGameStarting, true));

            // Fill in a player name with special characters
            var playerNameField = component.Find("input.Player_Name_Field");
            playerNameField.Change("Invalid@Name");

            var startGameButton = component.Find("button.Player_Name_Button");
            startGameButton.Click();

            var errorMessage = component.Find(".error-message");
            Assert.Equal("Name can only contain letters and numbers!", errorMessage.TextContent.Trim());
        }

        [Fact]
        public void StartGame_ShouldShowErrorForPlayerNameWithSpaces()
        {
            JSInterop.SetupVoid("initializeNotification");
            var component = RenderComponent<Home>(parameters => parameters.Add(p => p.isGameStarting, true));

            // Fill in a player name with spaces
            var playerNameField = component.Find("input.Player_Name_Field");
            playerNameField.Change("Player Name");

            var startGameButton = component.Find("button.Player_Name_Button");
            startGameButton.Click();

            var errorMessage = component.Find(".error-message");
            Assert.Equal("Name cannot contain spaces!", errorMessage.TextContent.Trim());
        }

        int current_player_id_tests = 4;

        [Fact]
        public async Task StartGame_ShouldSucceedWithUniquePlayerName()
        {
            JSInterop.SetupVoid("initializeNotification");
            string uniqueUsername = $"Player{DateTime.Now.Ticks}";

            var component = RenderComponent<Home>(parameters => parameters.Add(p => p.isGameStarting, true));
            var playerNameField = component.Find("input.Player_Name_Field");
            playerNameField.Change(uniqueUsername);

            var startGameButton = component.Find("button.Player_Name_Button");
            startGameButton.Click();

            var playerId = component.Instance.current_player_id;

            Console.WriteLine($"Player ID: {playerId}");

            component.WaitForAssertion(() =>
            {
                Assert.NotEqual(0, component.Instance.current_player_id);
            });
            component.WaitForAssertion(() =>
            {
                Assert.Equal(false, component.Instance.isGameStarting);
            });
            current_player_id_tests = playerId;
        }

        [Fact]
        public async Task FetchPlayerScore_ShouldReturnPlayerScore()
        {
            JSInterop.SetupVoid("initializeNotification");
            var component = RenderComponent<Home>(parameters => parameters.Add(p => p.isGameStarting, false));

            component.Instance.current_player_id = current_player_id_tests;

            await component.Instance.fetch_player_score();

            // await Task.Delay(500);

            Assert.True(component.Instance.current_player_score > 0);
        }

        [Fact]
        public async Task PlayAgain_ShouldReloadGame()
        {
            JSInterop.SetupVoid("initializeNotification");
            var component = RenderComponent<Home>(parameters => parameters.Add(p => p.isEnded, true).Add(p => p.isGameStarting, false));

            var playAgainButton = component.WaitForElement(".Play_Again");
            playAgainButton.Click();

            component.WaitForAssertion(() =>
            {
                Assert.Equal(true, component.Instance.isGameStarting);
                Assert.Equal(false, component.Instance.isEnded);
            });
        }

        [Fact]
        public async Task FetchOrderList_ShouldReturnValidOrder()
        {
            JSInterop.SetupVoid("initializeNotification");
            var component = RenderComponent<Home>(parameters => parameters.Add(p => p.isGameStarting, false));

            await component.Instance.fetch_order_list();

            // Assert that order list is fetched and not empty
            component.WaitForAssertion(() =>
            {
                Assert.NotEmpty(component.Instance.current_order_list);
            });
        }

        [Fact]
        public async Task FetchOrderPrice_ShouldReturnValidPrice()
        {
            JSInterop.SetupVoid("initializeNotification");
            var component = RenderComponent<Home>(parameters => parameters.Add(p => p.isGameStarting, false));

            await component.Instance.fetch_order_price();

            // Assert that the price is fetched and greater than 0
            component.WaitForAssertion(() =>
            {
                Assert.True(component.Instance.current_order_price > 0);
            });
        }

        [Fact]
        public async Task CheckList_ShouldReturnValidComparisonResult()
        {
            // Arrange
            JSInterop.SetupVoid("initializeNotification"); 

            var component = RenderComponent<Home>(parameters =>
                parameters.Add(p => p.isGameStarting, false)
                          .Add(p => p.isEnded, false));  // Game is running

            var stringListService = component.Services.GetRequiredService<GlobalStringListService>();

            await component.InvokeAsync(() =>
            {
                stringListService.AddString("Bun Bottom");
                stringListService.AddString("Portobello Mushroom Patty");
                stringListService.AddString("Hot Sauce");
                stringListService.AddString("Fish Patty");
                stringListService.AddString("Aioli");
                stringListService.AddString("Avocado");
                stringListService.AddString("Ketchup");
                stringListService.AddString("Sesame Bun");
            });

            component.Instance.current_order_list = new List<string> {"Portobello Mushroom Patty","Hot Sauce","Sesame Bun","Fish Patty","Aioli","Avocado","Ketchup" };
            component.Instance.current_player_score = 50; 
            component.Instance.current_order_price = 500; 

            // Trigger the CheckList function
            await component.InvokeAsync(async () => await component.Instance.CheckList());

            await Task.Delay(500);

            component.WaitForAssertion(() =>
            {
                Assert.Equal("Perfect Order!", component.Instance.message);
                Assert.True(component.Instance.current_player_score > 50);  
            });
        }

    }
}