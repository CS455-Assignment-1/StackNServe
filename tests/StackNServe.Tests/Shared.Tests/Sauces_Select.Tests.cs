// using Xunit;
// using Bunit;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.AspNetCore.Components.Web;
// using StackNServe.Services;
// using StackNServe.Pages;
// using System.Threading.Tasks;
// using StackNServe.Shared;
// namespace StackNServe.Tests
// {
//     public class SaucesSelectComponentTests : TestContext
//     {
//         public SaucesSelectComponentTests()
//         {
//             Services.AddSingleton<GlobalStringListService>();
//             Services.AddSingleton<SelectionButtonService>();
//         }

//         [Fact]
//         public void SaucesSelect_Should_Render_Correctly()
//         {
//             // Arrange
//             var cut = RenderComponent<Sauces_Select>();

//             // Assert
//             cut.Find("button.SaucesToggleButton");
//             cut.Find("img.Sauces_Select_Image");
//             Assert.False(cut.Instance.isExpanded);
//         }

//         [Fact]
//         public async Task SaucesSelect_ToggleMenu_Should_Toggle_ExpandedState()
//         {
//             // Arrange
//             var cut = RenderComponent<Sauces_Select>();

//             // Act
//             var button = cut.Find("button.SaucesToggleButton");
//             await cut.InvokeAsync(() => button.Click());

//             // Assert
//             Assert.NotEmpty(cut.FindAll("ul.ClickExpandMenu"));

//             // Act again to close the menu
//             await cut.InvokeAsync(() => button.Click());

//             // Assert
//             Assert.Empty(cut.FindAll("ul.ClickExpandMenu"));
//         }

//         [Fact]
//         public async Task Hovering_Over_All_Sauces_Should_Display_Info()
//         {
//             // Arrange
//             var cut = RenderComponent<Sauces_Select>();

//             // List of expected sauces names
//             var expectedSauceNames = new List<string>
//             {
//                 "Aioli", "BBQ Sauce", "Hot Sauce", "Ketchup",
//                 "Mayo", "Mustard", "Ranch"
//             };

//             for (int i = 0; i < expectedSauceNames.Count; i++)
//             {
//                 // Expand the menu first by clicking the toggle button
//                 var button = cut.Find("button.SaucesToggleButton");
//                 await cut.InvokeAsync(() => button.Click());

//                 // Find all the <li> elements
//                 var sauceItemList = cut.FindAll("li");

//                 // Simulate the mouse enter event
//                 await cut.InvokeAsync(() => sauceItemList[i].TriggerEventAsync("onmouseenter", new MouseEventArgs()));

//                 // Assert that the hover info is displayed correctly
//                 Assert.NotNull(cut.Instance.currentHoverInfo);
//                 Assert.Equal(expectedSauceNames[i], cut.Instance.currentHoverInfo.Name);

//                 // Simulate the mouse leave event
//                 await cut.InvokeAsync(() => sauceItemList[i].TriggerEventAsync("onmouseleave", new MouseEventArgs()));

//                 // Assert that the hover info is cleared
//                 Assert.Null(cut.Instance.currentHoverInfo);

//                 // Close the menu before moving to the next sauce
//                 await cut.InvokeAsync(() => button.Click());
//             }
//         }

//         [Fact]
//         public async Task SaucesSelect_ToggleMenu_Should_Update_SelectionButtonService_State()
//         {
//             // Arrange
//             var selectionButtonService = new SelectionButtonService();
//             Services.AddSingleton(selectionButtonService);
//             var cut = RenderComponent<Sauces_Select>();

//             // Act
//             var button = cut.Find("button.SaucesToggleButton");
//             await cut.InvokeAsync(() => button.Click());

//             // Assert
//             Assert.True(selectionButtonService.SaucesSelectVar);
//             Assert.False(selectionButtonService.BunSelectVar);
//             Assert.False(selectionButtonService.PattySelectVar);
//             Assert.False(selectionButtonService.ToppingSelectVar);
//         }

//         [Fact]
//         public async Task AddToBurger_Should_Add_Correct_Sauce_To_StringListService()
//         {
//             // Arrange
//             var stringListService = new GlobalStringListService();
//             Services.AddSingleton(stringListService);
//             var cut = RenderComponent<Sauces_Select>();

//             // Act
//             await cut.InvokeAsync(() => cut.Instance.AddToBurger("images/Sauces/Aioli.png"));

//             // Assert
//             Assert.Contains("Aioli", stringListService.StringList);
//         }

//         [Fact]
//         public async Task SaucesSelect_Should_Close_When_SaucesSelectVar_False()
//         {
//             // Arrange
//             var selectionButtonService = new SelectionButtonService();
//             Services.AddSingleton(selectionButtonService);
//             var cut = RenderComponent<Sauces_Select>();

//             // Act
//             var button = cut.Find("button.SaucesToggleButton");
//             await cut.InvokeAsync(() => button.Click());

//             // Simulate setting selectionButtonService.SaucesSelectVar to false
//             selectionButtonService.SaucesSelectVar = false;

//             // Wait for the timer to check and close the menu
//             await Task.Delay(200); // Small delay to simulate timer checking

//             // Assert that the menu is collapsed by checking the absence of the <ul> element
//             Assert.Empty(cut.FindAll("ul.ClickExpandMenu"));
//         }
//     }
// }
