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
    public class CookingTableComponentTests : TestContext
    {
        [Fact]
        public void CookingTableComponent_PageRenders()
        {
            var component = RenderComponent<Cooking_Table>();
            var cookingTable = component.Find("div.Cooking_Table");
            Assert.NotNull(cookingTable);
        }
    }
}