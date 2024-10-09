using Bunit;
using Xunit;
using StackNServe.Shared;
using Microsoft.AspNetCore.Components;

namespace StackNServe.Tests
{
    public class SubmitComponentTests : TestContext
    {
        [Fact]
        public void SubmitComponent_RendersCorrectly()
        {
            var component = RenderComponent<Submit>();

            var submitDiv = component.Find("div.Submit");
            var submitHeading = component.Find("h1.Submit_Heading");

            Assert.NotNull(submitDiv);
            Assert.NotNull(submitHeading);
            Assert.Equal("Submit", submitHeading.TextContent);
        }
        [Fact]
        public void SubmitComponent_InvokesOnSubmit_WhenClicked()
        {
            
            var onSubmitCalled = false;
            var component = RenderComponent<Submit>(parameters => parameters.Add(p => p.OnSubmit, EventCallback.Factory.Create(this, () => onSubmitCalled = true)));

            var submitDiv = component.Find("div.Submit");
            submitDiv.Click();

            Assert.True(onSubmitCalled);
        }
    }
}