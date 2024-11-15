using Bunit;
using Xunit;
using StackNServe.Pages;
using StackNServe.Services;
using StackNServe.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
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
    public class PlayAgainComponentTests : TestContext
    {
        [Fact]
        public void PlayAgainComponent_RendersCorrectly()
        {
            var component = RenderComponent<Play_Again>();

            var playAgainDiv = component.Find("div.Play_Again");
            var playAgainHeading = component.Find("h1.Play_Again_Heading");

            Assert.NotNull(playAgainDiv);
            Assert.NotNull(playAgainHeading);
            Assert.Equal("PLAY", playAgainHeading.TextContent);
        }        
        [Fact]
        public void PlayAgainComponent_NavigatesToNewGame_WhenClicked()
        {
            var mockNavigationManager = Services.GetRequiredService<NavigationManager>();
            var component = RenderComponent<Play_Again>();

            component.Find("div.Play_Again").Click();

            Assert.Equal("https://stacknserve.onrender.com/New_Game", mockNavigationManager.Uri);  
        }
    }
}