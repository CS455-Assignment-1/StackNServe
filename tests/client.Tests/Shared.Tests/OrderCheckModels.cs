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
    public class OrderCheckTests : TestContext
    {
        [Fact]
        public void OrderCheckRequest_CanSetAndGetProperties()
        {
            var userOrderList = new List<string> { "Bun", "Patty", "Sauce" };
            var requiredOrderList = new List<string> { "Bun", "Patty", "Lettuce" };
            var currentPlayerScore = 100;
            var orderPrice = 50;

            var orderCheckRequest = new OrderCheckRequest
            {
                UserOrderList = userOrderList,
                RequiredOrderList = requiredOrderList,
                CurrentPlayerScore = currentPlayerScore,
                OrderPrice = orderPrice
            };

            Assert.Equal(userOrderList, orderCheckRequest.UserOrderList);
            Assert.Equal(requiredOrderList, orderCheckRequest.RequiredOrderList);
            Assert.Equal(currentPlayerScore, orderCheckRequest.CurrentPlayerScore);
            Assert.Equal(orderPrice, orderCheckRequest.OrderPrice);
        }

        [Fact]
        public void OrderCheckResponse_CanSetAndGetProperties()
        {
            var isOrderCorrect = true;
            var finalScore = 120;
            var message = "Order is correct!";

            var orderCheckResponse = new OrderCheckResponse
            {
                IsOrderCorrect = isOrderCorrect,
                FinalScore = finalScore,
                Message = message
            };

            Assert.Equal(isOrderCorrect, orderCheckResponse.IsOrderCorrect);
            Assert.Equal(finalScore, orderCheckResponse.FinalScore);
            Assert.Equal(message, orderCheckResponse.Message);
        }
}

}