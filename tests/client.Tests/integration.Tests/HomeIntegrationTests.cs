// using Xunit;
// using Bunit;
// using StackNServe.Pages;
// using StackNServe.Services;
// using Microsoft.Extensions.DependencyInjection;
// using System.Net.Http;
// using System.Text;
// using System.Text.Json;
// using System.Threading.Tasks;

// namespace StackNServe.Tests
// {
//     public class HomeIntegrationTests : TestContext
//     {
//         private HttpClient realHttpClient;
//         private GlobalStringListService stringListService;

//         public HomeIntegrationTests()
//         {
//             realHttpClient = new HttpClient { BaseAddress = new Uri("http://localhost:8000/") };
//             stringListService = new GlobalStringListService();
//             Services.AddSingleton(realHttpClient);
//             Services.AddSingleton(stringListService);
//             Services.AddSingleton<SelectionButtonService>();
//         }

//         public static async Task<bool> WaitForConditionAsync(Func<Task<bool>> condition, TimeSpan timeout)
//         {
//             var startTime = DateTime.Now;
//             while (DateTime.Now - startTime < timeout)
//             {
//                 if (await condition())
//                 {
//                     return true;
//                 }

//                 // Wait before checking the condition again
//                 await Task.Delay(100);
//             }

//             return false;
//         }


//         [Fact]
//         public void StartGame_ShouldShowErrorForEmptyPlayerName()
//         {
//             JSInterop.SetupVoid("initializeNotification");

//             var component = RenderComponent<Home>(parameters => parameters
//                 .Add(p => p.isGameStarting, true)
//             );

//             // Leave player name empty
//             var playerNameField = component.Find("input.Player_Name_Field");
//             playerNameField.Change("");

//             var startGameButton = component.Find("button.Player_Name_Button");
//             startGameButton.Click();

//             var errorMessage = component.Find(".error-message");
//             Assert.Equal("Name cannot be empty!", errorMessage.TextContent.Trim());
//         }

//         [Fact]
//         public void StartGame_ShouldShowErrorForInvalidPlayerName()
//         {
//             JSInterop.SetupVoid("initializeNotification");

//             var component = RenderComponent<Home>(parameters => parameters
//                 .Add(p => p.isGameStarting, true)
//             );

//             // Fill in a player name with special characters
//             var playerNameField = component.Find("input.Player_Name_Field");
//             playerNameField.Change("Invalid@Name");

//             var startGameButton = component.Find("button.Player_Name_Button");
//             startGameButton.Click();

//             var errorMessage = component.Find(".error-message");
//             Assert.Equal("Name can only contain letters and numbers!", errorMessage.TextContent.Trim());
//         }

//         [Fact]
//         public void StartGame_ShouldShowErrorForPlayerNameWithSpaces()
//         {
//             JSInterop.SetupVoid("initializeNotification");
//             var component = RenderComponent<Home>(parameters => parameters.Add(p => p.isGameStarting, true));

//             // Fill in a player name with spaces
//             var playerNameField = component.Find("input.Player_Name_Field");
//             playerNameField.Change("Player Name");

//             var startGameButton = component.Find("button.Player_Name_Button");
//             startGameButton.Click();

//             var errorMessage = component.Find(".error-message");
//             Assert.Equal("Name cannot contain spaces!", errorMessage.TextContent.Trim());
//         }

//         [Fact]
//         public async Task StartGame_ShouldShowErrorForDuplicatePlayerName()
//         {
//             JSInterop.SetupVoid("initializeNotification");
//             var component = RenderComponent<Home>(parameters => parameters.Add(p => p.isGameStarting, true));

//             // Enter a player name that is already taken
//             var playerNameField = component.Find("input.Player_Name_Field");
//             playerNameField.Change("w");

//             var startGameButton = component.Find("button.Player_Name_Button");
//             startGameButton.Click();

//             await Task.Delay(500);

//             var errorMessage = component.Find(".error-message");
//             Assert.Equal("Name already exists!", errorMessage.TextContent.Trim());
//         }

//         [Fact]
//         public async Task StartGame_ShouldSucceedWithUniquePlayerName()
//         {
//             JSInterop.SetupVoid("initializeNotification");

//             string uniqueUsername = $"Player{DateTime.Now.Ticks}";

//             var component = RenderComponent<Home>(parameters => parameters.Add(p => p.isGameStarting, true));
//             var playerNameField = component.Find("input.Player_Name_Field");
//             playerNameField.Change(uniqueUsername);

//             var startGameButton = component.Find("button.Player_Name_Button");
//             startGameButton.Click();

//             var playerId = component.Instance.current_player_id;

//             Assert.True(playerId > 0);
//             Assert.Empty(component.Instance.error_message);
//         }

//         [Fact]
//         public async Task FetchPlayerScore_ShouldReturnPlayerScore()
//         {
//             JSInterop.SetupVoid("initializeNotification");
//             var component = RenderComponent<Home>(parameters => parameters.Add(p => p.isGameStarting, false));

//             component.Instance.current_player_id = 15;

//             await component.Instance.fetch_player_score();

//             // Assert that score is positive
//             Assert.True(component.Instance.current_player_score > 0);
//         }

//         [Fact]
//         public async Task UpdatePlayerScore_ShouldChangeScore()
//         {
//             JSInterop.SetupVoid("initializeNotification");
//             var component = RenderComponent<Home>(parameters => parameters.Add(p => p.isGameStarting, false));

//             component.Instance.current_player_id = 15;

//             await component.Instance.fetch_player_score();

//             // Assert that initial score is positive
//             Assert.True(component.Instance.current_player_score > 0);

//             component.Instance.current_player_score = 150;
//             await component.Instance.update_player_score();

//             // Act: Fetch the updated score from the server
//             await component.Instance.fetch_player_score();

//             // Assert: Check if the updated score was fetched correctly
//             Assert.Equal(150, component.Instance.current_player_score);
//         }
//     }
// }