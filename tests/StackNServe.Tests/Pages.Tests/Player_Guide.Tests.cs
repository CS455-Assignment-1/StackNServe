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
    public class PlayerGuideTests : TestContext
    {
        [Fact]
        public void PlayerGuidePage_PageRenders()
        {
            var component = RenderComponent<Player_Guide>();
            
            var heading = component.Find("h1.Player_Guide_Heading");
            var greeting = component.Find("p.Player_Guide_Greeting");
            var rules = component.Find("div.Player_Guide_Rules");
            var intent = component.Find("p.Player_Guide_Intent");
            var playAgainButton = component.Find("div.Play_Game_Button");

            Assert.NotNull(heading);
            Assert.NotNull(greeting);
            Assert.NotNull(rules);
            Assert.NotNull(intent);
            Assert.NotNull(playAgainButton);
        }

        public void PlayerGuidePage_HeadingContentIsCorrect()
        {
            var component = RenderComponent<Player_Guide>();
            var heading = component.Find("h1.Player_Guide_Heading");

            Assert.Equal("Player Guide", heading.TextContent);
        }

        [Fact]
        public void PlayerGuidePage_RulesContentIsCorrect()
        {
            var component = RenderComponent<Player_Guide>();

            var rules = component.Find("div.Player_Guide_Rules");
            var ruleItems = component.FindAll("li");

            Assert.Equal(7, ruleItems.Count); 
        }

        [Fact]
        public void PlayerGuidePage_IntentContentIsCorrect()
        {
            var component = RenderComponent<Player_Guide>();

            var intent = component.Find("p.Player_Guide_Intent");
            Assert.Equal("The intent of the game is to test your memory and speed. \n        You need to remember where to find the ingredients asked for, and their prices.\n        You also need to be quick in making the burgers and serving them to the customers.\n        You need to calculate if the current order is profitable or not.\n        There is no option to remove any wrong ingredients you may have stacked. So, be careful while stacking the ingredients.", intent.TextContent.Trim());
        }

    }   
}