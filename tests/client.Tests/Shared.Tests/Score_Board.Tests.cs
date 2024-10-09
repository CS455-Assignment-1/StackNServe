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
    public class ScoreBoardComponentTests : TestContext
    {
        [Fact]
        public void ScoreBoardComponent_RendersCorrectly()
        {
            var component = RenderComponent<Score_Board>();

            var scoreBoardDiv = component.Find("div.Score_Board");
            var scoreBoardImage = component.Find("img.Score_Board_Image");
            var scoreText = component.Find("h4.Current_Score");

            Assert.NotNull(scoreBoardDiv);
            Assert.NotNull(scoreBoardImage);
            Assert.NotNull(scoreText);
            Assert.Equal("images/Rupee_Symbol.png", scoreBoardImage.GetAttribute("src"));
            Assert.Equal("100", scoreText.TextContent);
        }
        [Fact]
        public void ScoreBoardComponent_DisplaysCorrectScore()
        {
            var customScore = 250;

            var component = RenderComponent<Score_Board>(parameters => parameters.Add(p => p.current_score, customScore));

            var scoreText = component.Find("h4.Current_Score");
            Assert.Equal(customScore.ToString(), scoreText.TextContent);
        }
    }
}