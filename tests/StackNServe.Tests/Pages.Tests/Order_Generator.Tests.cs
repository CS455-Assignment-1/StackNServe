// using System;
// using System.Collections.Generic;
// using Xunit;
// using StackNServe.Pages;
// using StackNServe.Services;
// using StackNServe.Shared;

// namespace StackNServe.Tests
// {
//     public class BurgerComponentsTests
//     {
//         [Fact]
//         public void TestBurgerComponentsInitialization()
//         {
//             // Arrange & Act
//             var burger = new BurgerComponents();

//             // Assert
//             Assert.NotEmpty(burger.Order_List); // Check if the Order_List is not empty
//             Assert.Contains(burger.Order_List, item => item.Contains("Bun")); // Check if the order contains a bun
//         }

//         [Fact]
//         public void TestSecureRandomNumberWithinRange()
//         {
//             // Arrange
//             var burger = new BurgerComponents();

//             // Act
//             int randomNumber = burger.GetSecureRandomNumber(1, 5);

//             // Assert
//             Assert.InRange(randomNumber, 1, 4); // Ensure the random number is within the expected range
//         }

//         [Fact]
//         public void TestRandomizeList()
//         {
//             // Arrange
//             var burger = new BurgerComponents();
//             List<string> list = new List<string> { "Item1", "Item2", "Item3", "Item4" };
//             List<string> originalList = new List<string>(list);

//             // Act
//             var randomizedList = burger.RandomizeList(list);

//             // Assert
//             Assert.NotEqual(originalList, randomizedList); // Check that the list has been randomized
//             Assert.Equal(originalList.Count, randomizedList.Count); // Check that the size remains the same
//             Assert.True(new HashSet<string>(randomizedList).SetEquals(originalList)); // Ensure it still contains the same items
//         }

//         [Fact]
//         public void TestOrderListContainsAtLeastOnePatty()
//         {
//             // Arrange & Act
//             var burger = new BurgerComponents();

//             // Assert
//             Assert.Contains(burger.Order_List, item => item.Contains("Patty")); // Check if there's at least one patty
//         }
//     }
// }
