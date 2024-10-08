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
//     public class ToppingsSelectComponentTests : TestContext
//     {
//         public ToppingsSelectComponentTests()
//         {
//             Services.AddSingleton<GlobalStringListService>();
//             Services.AddSingleton<SelectionButtonService>();
//         }

//         [Fact]
//         public void ToppingsSelect_Should_Render_Correctly()
//         {
//             // Arrange
//             var cut = RenderComponent<Toppings_Select>();

//             // Assert
//             cut.Find("button.ToppingToggleButton");
//             cut.Find("img.Topping_Select_Image");
//             Assert.False(cut.Instance.isExpanded);
//         }

//         [Fact]
//         public async Task ToppingsSelect_ToggleMenu_Should_Toggle_ExpandedState()
//         {
//             // Arrange
//             var cut = RenderComponent<Toppings_Select>();

//             // Act
//             var button = cut.Find("button.ToppingToggleButton");
//             await cut.InvokeAsync(() => button.Click());

//             // Assert
//             Assert.True(cut.Instance.isExpanded);
//             cut.Find("ul.ClickExpandMenu");

//             // Act again to close the menu
//             await cut.InvokeAsync(() => button.Click());

//             // Assert
//             Assert.False(cut.Instance.isExpanded);
//             Assert.Empty(cut.FindAll("ul.ClickExpandMenu"));
//         }

//         [Fact]
//         public async Task Hovering_Over_All_Toppings_Should_Display_Info()
//         {
//             // Arrange
//             var cut = RenderComponent<Toppings_Select>();

//             for (int i = 0; i < 9; i++) // Loop through all 9 toppings
//             {
//                 // Expand the menu first by clicking the toggle button
//                 var button = cut.Find("button.ToppingToggleButton");
//                 await cut.InvokeAsync(() => button.Click());

//                 // Expected topping names
//                 var expectedToppingNames = new List<string> 
//                 { 
//                     "Avocado", "Bacon", "Cheese", "Egg", 
//                     "Jalapenos", "Lettuce", "Onion", 
//                     "Pickles", "Tomato" 
//                 };

//                 // Find all the <li> elements
//                 var toppingItemList = cut.FindAll("li");

//                 // Simulate the mouse enter event
//                 await cut.InvokeAsync(() => toppingItemList[i].TriggerEventAsync("onmouseenter", new MouseEventArgs()));

//                 // Assert that the hover info is displayed correctly
//                 Assert.NotNull(cut.Instance.currentHoverInfo);
//                 Assert.Equal(expectedToppingNames[i], cut.Instance.currentHoverInfo.Name);

//                 // Simulate the mouse leave event
//                 await cut.InvokeAsync(() => toppingItemList[i].TriggerEventAsync("onmouseleave", new MouseEventArgs()));

//                 // Assert that the hover info is cleared
//                 Assert.Null(cut.Instance.currentHoverInfo);

//                 // Close the menu before moving to the next topping
//                 await cut.InvokeAsync(() => button.Click());
//             }
//         }

//         [Fact]
//         public async Task ToppingsSelect_ToggleMenu_Should_Update_SelectionButtonService_State()
//         {
//             // Arrange
//             var selectionButtonService = new SelectionButtonService();
//             Services.AddSingleton(selectionButtonService);
//             var cut = RenderComponent<Toppings_Select>();

//             // Act
//             var button = cut.Find("button.ToppingToggleButton");
//             await cut.InvokeAsync(() => button.Click());

//             // Assert
//             Assert.True(selectionButtonService.ToppingSelectVar);
//             Assert.False(selectionButtonService.BunSelectVar);
//             Assert.False(selectionButtonService.PattySelectVar);
//             Assert.False(selectionButtonService.SaucesSelectVar);
//         }

//         [Fact]
//         public async Task AddToBurger_Should_Add_Correct_Topping_To_StringListService()
//         {
//             // Arrange
//             var stringListService = new GlobalStringListService();
//             Services.AddSingleton(stringListService);
//             var cut = RenderComponent<Toppings_Select>();

//             // Act
//             await cut.InvokeAsync(() => cut.Instance.AddToBurger("images/Toppings/Avocado.png"));

//             // Assert
//             Assert.Contains("Avocado", stringListService.StringList);
//         }

//         [Fact]
//         public async Task ToppingsSelect_Should_Close_When_ToppingSelectVar_False()
//         {
//             // Arrange
//             var selectionButtonService = new SelectionButtonService();
//             Services.AddSingleton(selectionButtonService);
//             var cut = RenderComponent<Toppings_Select>();

//             // Act
//             var button = cut.Find("button.ToppingToggleButton");
//             await cut.InvokeAsync(() => button.Click());

//             // Simulate selectionButtonService.ToppingSelectVar being set to false
//             selectionButtonService.ToppingSelectVar = false;

//             // Wait for the timer to check and close the menu
//             await Task.Delay(200); // Small delay to simulate timer checking

//             // Assert
//             Assert.False(cut.Instance.isExpanded);
//         }
//     }
// }
