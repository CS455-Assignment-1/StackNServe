using Bunit;
using Xunit;
using StackNServe.Pages;
using StackNServe.Services;
using StackNServe.Shared;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
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
    public class SkipComponentTests : TestContext
    {
        [Fact]
        public void SkipComponent_RendersCorrectly()
        {
            var component = RenderComponent<Skip>();

            var skipDiv = component.Find("div.Skip");
            var skipHeading = component.Find("h1.Skip_Heading");

            Assert.NotNull(skipDiv);
            Assert.NotNull(skipHeading);
            Assert.Equal("Skip", skipHeading.TextContent);
        }

        [Fact]
        public void SkipComponent_InvokesOnSkip_WhenClicked()
        {
            var onSkipCalled = false;
            var component = RenderComponent<Skip>(parameters => parameters.Add(p => p.OnSkip, EventCallback.Factory.Create(this, () => onSkipCalled = true)));

            var skipDiv = component.Find("div.Skip");
            skipDiv.Click();

            Assert.True(onSkipCalled);
        }
    }
}
