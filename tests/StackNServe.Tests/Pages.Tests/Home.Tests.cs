using Bunit;
using Xunit;
using StackNServe.Pages;
using StackNServe.Services;
using StackNServe.Models;
using System.Security.Cryptography;
using StackNServe.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;

namespace StackNServe.Tests
{
    public class HomeTests : TestContext
    {
        private Mock<GlobalStringListService> _mockStringListService;
        private Mock<IJSRuntime> _mockJSRuntime;
        private Mock<SelectionButtonService> _mockSelectionButtonService;

        public HomeTests()
        {
            _mockStringListService = new Mock<GlobalStringListService>();
            _mockJSRuntime = new Mock<IJSRuntime>();

            Services.AddSingleton(_mockStringListService.Object);
            Services.AddSingleton(_mockJSRuntime.Object);
            Services.AddSingleton<SelectionButtonService>();
        }

        [Fact]
        public void InitialRender_ShouldShowGameElements()
        {
            // Arrange
            var cut = RenderComponent<Home>();

            // Act & Assert
            cut.FindComponent<Bun_Select>();
            cut.FindComponent<Patty_Select>();
            cut.FindComponent<Toppings_Select>();
            cut.FindComponent<Sauces_Select>();
            cut.FindComponent<Main_Page_Header>();
            cut.FindComponent<CountdownTimer>();
            cut.FindComponent<Cooking_Table>();
            cut.FindComponent<Order>();
            cut.FindComponent<Skip>();
            cut.FindComponent<Submit>();
            cut.FindComponent<Score_Board>();
        }
        [Fact]
        public async Task PlayerSkip_ShouldDecrease10FromBalance()
        {
            // Arrange
            var cut = RenderComponent<Home>();

            // Act
            await cut.InvokeAsync(() => cut.Instance.Player_Skip());

            // Assert
            Assert.Equal(90, cut.Instance.New_Player.Balance);
        }

        [Fact]
        public async Task HandleTimerFinished_ShouldChangeIsEndedToTrue()
        {
            // Arrange
            var cut = RenderComponent<Home>();

            // Act
            await cut.InvokeAsync(() => cut.Instance.HandleTimerFinished());

            // Assert
            Assert.True(cut.Instance.isEnded);
        }

        [Fact]
        public async Task FetchOrderList_ShouldReturnListOfOrders()
        {
            // Arrange
            var cut = RenderComponent<Home>();

            // Act
            var result = cut.Instance.Fetch_Order_List();

            // Assert
            Assert.IsType<List<string>>(result);
        }

        [Fact]
        public async Task FetchOrderPrice_ShouldReturnPriceBetween5And200()
        {
            // Arrange
            var cut = RenderComponent<Home>();

            // Act
            var result = cut.Instance.Fetch_Order_Price();

            // Assert
            Assert.InRange(result, 5, 200);
        }

        [Fact]
        public async Task ReloadGame_ShouldResetGame()
        {
            // Arrange
            var cut = RenderComponent<Home>();
            cut.Instance.isEnded = true;
            cut.Instance.New_Player.Balance = 50;

            // Act
            await cut.InvokeAsync(() => cut.Instance.ReloadGame());

            // Assert
            Assert.False(cut.Instance.isEnded);
            Assert.Equal(100, cut.Instance.New_Player.Balance);
        }

        [Fact]
        public async Task UpdateOrder_ShouldUpdateOrder()
        {
            // Arrange
            var cut = RenderComponent<Home>();

            // Act
            cut.InvokeAsync(() => cut.Instance.UpdateOrder());

            // Assert
            Assert.IsType<List<string>>(cut.Instance.currentOrderList);
            Assert.InRange(cut.Instance.currentOrderPrice, 5, 200);
        }
        [Fact]
        public void PlayerSkip_CheckFunctionsCalled() 
        {
            var mockStringListService = new Mock<GlobalStringListService>();
            mockStringListService.Setup(s => s.ClearList()).Verifiable();

            // Setup
            Services.AddSingleton(mockStringListService.Object);

            // Arrange
            var cut = RenderComponent<Home>();

            // Act
            cut.InvokeAsync(() => cut.Instance.Player_Skip());

            // Assert
            Assert.Equal(90, cut.Instance.New_Player.Balance);
            mockStringListService.Verify(s => s.ClearList(), Times.Once);
        }
    }
}