using StackNServe.Services;
using Xunit;
using System;

namespace StackNServe.Tests
{
    public class SelectionButtonServiceTests
    {
        [Fact]
        public void BunSelectVar_Should_Set_And_Get_Value()
        {
            // Arrange
            var service = new SelectionButtonService();
            
            // Act
            service.BunSelectVar = true;

            // Assert
            Assert.True(service.BunSelectVar);
        }

        [Fact]
        public void PattySelectVar_Should_Set_And_Get_Value()
        {
            // Arrange
            var service = new SelectionButtonService();

            // Act
            service.PattySelectVar = true;

            // Assert
            Assert.True(service.PattySelectVar);
        }

        [Fact]
        public void SaucesSelectVar_Should_Set_And_Get_Value()
        {
            // Arrange
            var service = new SelectionButtonService();

            // Act
            service.SaucesSelectVar = true;

            // Assert
            Assert.True(service.SaucesSelectVar);
        }

        [Fact]
        public void ToppingSelectVar_Should_Set_And_Get_Value()
        {
            // Arrange
            var service = new SelectionButtonService();

            // Act
            service.ToppingSelectVar = true;

            // Assert
            Assert.True(service.ToppingSelectVar);
        }

        [Fact]
        public void NotifyStateChanged_Should_Invoke_OnChange_Event()
        {
            // Arrange
            var service = new SelectionButtonService();
            bool eventInvoked = false;

            service.OnChange += () => eventInvoked = true;

            // Act
            service.NotifyStateChanged();

            // Assert
            Assert.True(eventInvoked);
        }

        [Fact]
        public void NotifyStateChanged_Should_Not_Throw_If_No_Subscribers()
        {
            // Arrange
            var service = new SelectionButtonService();

            // Act & Assert
            var exception = Record.Exception(() => service.NotifyStateChanged());

            // Assert
            Assert.Null(exception);
        }
    }
}
