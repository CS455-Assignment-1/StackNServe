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
    public class MainPageHeaderTests : TestContext
    {
        [Fact]
        public void MainPageHeader_RendersCorrectly()
        {
            var component = RenderComponent<Main_Page_Header>();

            var imgElement = component.Find("img.Main_Page_Header_Image");

            Assert.NotNull(imgElement);
            Assert.Equal("images/Main_Page_Header.png", imgElement.GetAttribute("src"));
            Assert.Equal("Main Page Header", imgElement.GetAttribute("alt"));
            Assert.Equal("Main_Page_Header_Image", imgElement.GetAttribute("class"));
        }
    }
}