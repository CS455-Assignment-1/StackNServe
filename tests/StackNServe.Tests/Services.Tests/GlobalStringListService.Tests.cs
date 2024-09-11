using StackNServe.Services;
using Xunit;
using System.Linq;

namespace StackNServe.Tests
{
    public class GlobalStringListServiceTests
    {
        [Fact]
        public void AddString_ShouldAddStringToList_WhenValidStringIsProvided()
        {
            // Arrange
            var service = new GlobalStringListService();
            var testString = "TestItem";

            // Act
            service.AddString(testString);

            // Assert
            Assert.Contains(testString, service.StringList);
        }

        [Fact]
        public void AddString_ShouldNotAddString_WhenNullOrWhitespaceStringIsProvided()
        {
            // Arrange
            var service = new GlobalStringListService();

            // Act
            service.AddString(null);
            service.AddString("    ");

            // Assert
            Assert.Empty(service.StringList);
        }

        [Fact]
        public void RemoveString_ShouldRemoveStringFromList_WhenItemExists()
        {
            // Arrange
            var service = new GlobalStringListService();
            var testString = "TestItem";
            service.AddString(testString);

            // Act
            service.RemoveString(testString);

            // Assert
            Assert.DoesNotContain(testString, service.StringList);
        }

        [Fact]
        public void RemoveString_ShouldNotRemoveString_WhenItemDoesNotExist()
        {
            // Arrange
            var service = new GlobalStringListService();
            var testString = "NonExistentItem";

            // Act
            service.RemoveString(testString);

            // Assert
            Assert.Empty(service.StringList);
        }

        [Fact]
        public void ClearList_ShouldClearAllItemsFromList()
        {
            // Arrange
            var service = new GlobalStringListService();
            service.AddString("Item1");
            service.AddString("Item2");

            // Act
            service.ClearList();

            // Assert
            Assert.Empty(service.StringList);
        }

        [Fact]
        public void AddString_ShouldTriggerOnChange_WhenItemIsAdded()
        {
            // Arrange
            var service = new GlobalStringListService();
            var eventTriggered = false;
            service.OnChange += () => eventTriggered = true;

            // Act
            service.AddString("TestItem");

            // Assert
            Assert.True(eventTriggered);
        }

        [Fact]
        public void RemoveString_ShouldTriggerOnChange_WhenItemIsRemoved()
        {
            // Arrange
            var service = new GlobalStringListService();
            service.AddString("TestItem");
            var eventTriggered = false;
            service.OnChange += () => eventTriggered = true;

            // Act
            service.RemoveString("TestItem");

            // Assert
            Assert.True(eventTriggered);
        }

        [Fact]
        public void ClearList_ShouldTriggerOnChange_WhenListIsCleared()
        {
            // Arrange
            var service = new GlobalStringListService();
            service.AddString("Item1");
            service.AddString("Item2");
            var eventTriggered = false;
            service.OnChange += () => eventTriggered = true;

            // Act
            service.ClearList();

            // Assert
            Assert.True(eventTriggered);
        }
    }
}
