// using Bunit;
// using Xunit;
// using StackNServe.Pages;

// namespace StackNServe.Tests;
// public class PlayerGuideTests : TestContext
// {
//     [Fact]
//     public void PlayerGuide_Heading_Should_Render_Correctly()
//     {
//         // Arrange & Act
//         var component = RenderComponent<Player_Guide>(); 

//         // Assert
//         var heading = component.Find(".Player_Guide_Heading");
//         Assert.Equal("Player Guide", heading.TextContent);
//     }

//     [Fact]
//     public void PlayerGuide_Rules_Should_Render_Seven_Rules()
//     {
//         // Arrange & Act
//         var component = RenderComponent<Player_Guide>();

//         // Assert
//         var rules = component.Find(".Player_Guide_Rules");
//         Assert.Contains("Customers will come to your restaurant and order a burger.", rules.TextContent);
//         Assert.Contains("Your initial balance in the game is Rs. 100.", rules.TextContent);
//         Assert.Contains("The time allotted to make the burgers is 120 seconds.", rules.TextContent);

//         var listItems = component.FindAll("li");
//         Assert.Equal(7, listItems.Count); 
//     }

//     [Fact]
//     public void PlayerGuide_Intent_Should_Render_Correctly()
//     {
//         // Arrange & Act
//         var component = RenderComponent<Player_Guide>();

//         // Assert
//         var intentParagraph = component.Find(".Player_Guide_Intent");
//         Assert.Contains("The intent of the game is to test your memory and speed.", intentParagraph.TextContent);
//         Assert.Contains("You need to calculate if the current order is profitable or not.", intentParagraph.TextContent);
//     }

//     [Fact]
//     public void Play_Again_Button_Should_Render_Correctly()
//     {
//         // Arrange & Act
//         var component = RenderComponent<Player_Guide>();

//         // Assert
//         var playAgainButton = component.FindComponent<Play_Again>();
//         Assert.NotNull(playAgainButton); 
//     }
// }
