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
    public class SelectionButtonServiceTests : TestContext
    {
        [Fact]
        public void Setting_BunSelectVar_TriggersOnChangeEvent()
        {
            var service = new SelectionButtonService();
            var eventInvoked = false;

            service.OnChange += () => eventInvoked = true;
            service.BunSelectVar = true;
            service.NotifyStateChanged();

            Assert.True(eventInvoked);
        }

        [Fact]
        public void Setting_PattySelectVar_TriggersOnChangeEvent()
        {
            var service = new SelectionButtonService();
            var eventInvoked = false;

            service.OnChange += () => eventInvoked = true;
            service.PattySelectVar = true;
            service.NotifyStateChanged();

            Assert.True(eventInvoked);
        }

        [Fact]
        public void Setting_SaucesSelectVar_TriggersOnChangeEvent()
        {
            var service = new SelectionButtonService();
            var eventInvoked = false;

            service.OnChange += () => eventInvoked = true;
            service.SaucesSelectVar = true;
            service.NotifyStateChanged();

            Assert.True(eventInvoked);
        }

        [Fact]
        public void Setting_ToppingSelectVar_TriggersOnChangeEvent()
        {
            var service = new SelectionButtonService();
            var eventInvoked = false;

            service.OnChange += () => eventInvoked = true;
            service.ToppingSelectVar = true;
            service.NotifyStateChanged();

            Assert.True(eventInvoked);
        }

        [Fact]
        public void NotifyStateChanged_InvokesOnChangeEvent()
        {
            var service = new SelectionButtonService();
            var eventInvoked = false;

            service.OnChange += () => eventInvoked = true;
            service.NotifyStateChanged();

            Assert.True(eventInvoked);
        }

        [Fact]
        public void Initial_Values_AreFalse()
        {
            var service = new SelectionButtonService();

            Assert.False(service.BunSelectVar);
            Assert.False(service.PattySelectVar);
            Assert.False(service.SaucesSelectVar);
            Assert.False(service.ToppingSelectVar);
        }
    }
}
