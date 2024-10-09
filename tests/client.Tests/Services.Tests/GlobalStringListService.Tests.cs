using Bunit;
using Xunit;
using StackNServe.Pages;
using StackNServe.Services;
using StackNServe.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using Microsoft.JSInterop;
using Moq;
using Moq.Protected;
using System.Net.Http;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;

namespace StackNServe.Tests
{
    public class GlobalStringListServiceTests : TestContext
    {
        [Fact]
        public void AddString_AddsItemToStringList()
        {
            var service = new GlobalStringListService();
            var item = "Lettuce";

            service.AddString(item);

            Assert.Single(service.StringList);
            Assert.Equal("Lettuce", service.StringList[0]);
        }

        [Fact]
        public void AddString_DoesNotAddEmptyOrWhitespaceString()
        {
            var service = new GlobalStringListService();

            service.AddString("");  
            service.AddString(" "); 

            Assert.Empty(service.StringList);
        }

        [Fact]
        public void RemoveString_RemovesItemFromStringList()
        {
            var service = new GlobalStringListService();
            var item = "Lettuce";
            service.AddString(item);

            service.RemoveString(item);

            Assert.Empty(service.StringList);
        }

        [Fact]
        public void RemoveString_DoesNothingIfItemNotInList()
        {
            var service = new GlobalStringListService();
            service.AddString("Lettuce");

            service.RemoveString("Tomato");

            Assert.Single(service.StringList);
            Assert.Equal("Lettuce", service.StringList[0]);
        }

        [Fact]
        public void ClearList_ClearsAllItemsFromStringList()
        {
            var service = new GlobalStringListService();
            service.AddString("Lettuce");
            service.AddString("Tomato");

            service.ClearList();

            Assert.Empty(service.StringList);
        }

        [Fact]
        public void AddString_InvokesOnChangeEvent()
        {
            var service = new GlobalStringListService();
            var eventInvoked = false;

            service.OnChange += () => eventInvoked = true;
            service.AddString("Lettuce");

            Assert.True(eventInvoked);
        }

        [Fact]
        public void RemoveString_InvokesOnChangeEvent()
        {
            var service = new GlobalStringListService();
            var eventInvoked = false;

            service.OnChange += () => eventInvoked = true;
            service.AddString("Lettuce");
            service.RemoveString("Lettuce");

            Assert.True(eventInvoked);
        }

        [Fact]
        public void ClearList_InvokesOnChangeEvent()
        {
            var service = new GlobalStringListService();
            var eventInvoked = false;

            service.OnChange += () => eventInvoked = true;
            service.AddString("Lettuce");
            service.ClearList();

            Assert.True(eventInvoked);
        }

        [Fact]
        public void StringList_ReturnsReadOnlyList()
        {
            var service = new GlobalStringListService();
            var stringList = service.StringList;

            Assert.IsAssignableFrom<IReadOnlyList<string>>(stringList);
        }
    }
}
