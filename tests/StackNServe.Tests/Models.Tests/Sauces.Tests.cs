namespace StackNServe.Tests;

public class MustardTests
{
    [Fact]
    public void Mustard_IsDefined_True()
    {
        // Arrange & Act
        var mustard = new Mustard();

        // Assert
        Assert.True(mustard != null);
    }
    [Fact]
    public void Mustard_Ctor_SetsProperties()
    {
        // Arrange & Act
        var mustard = new Mustard();

        // Assert
        Assert.Equal(1, mustard.Id);
        Assert.Equal("Mustard", mustard.Name);
        Assert.Equal(2.00m, mustard.Price);
        Assert.Equal("../wwwroot/images/Sauces/Mustard.png", mustard.ImageUrl);
        Assert.Equal("Mustard is a condiment that is often used on sandwiches and hot dogs. It is made from the seeds of the mustard plant.", mustard.Description);
    }
}

public class KetchupTests
{
    [Fact]
    public void Ketchup_IsDefined_True()
    {
        // Arrange & Act
        var ketchup = new Ketchup();

        // Assert
        Assert.True(ketchup != null);
    }
    [Fact]
    public void Ketchup_Ctor_SetsProperties()
    {
        // Arrange & Act
        var ketchup = new Ketchup();

        // Assert
        Assert.Equal(2, ketchup.Id);
        Assert.Equal("Ketchup", ketchup.Name);
        Assert.Equal(2.00m, ketchup.Price);
        Assert.Equal("../wwwroot/images/Sauces/Ketchup.png", ketchup.ImageUrl);
        Assert.Equal("Ketchup is a sweet and tangy condiment that is often used on burgers and fries. It is made from tomatoes, vinegar, and sugar.", ketchup.Description);
    }
}

public class MayoTests
{
    [Fact]
    public void Mayo_IsDefined_True()
    {
        // Arrange & Act
        var mayo = new Mayo();

        // Assert
        Assert.True(mayo != null);
    }
    [Fact]
    public void Mayo_Ctor_SetsProperties()
    {
        // Arrange & Act
        var mayo = new Mayo();

        // Assert
        Assert.Equal(3, mayo.Id);
        Assert.Equal("Mayo", mayo.Name);
        Assert.Equal(2.00m, mayo.Price);
        Assert.Equal("../wwwroot/images/Sauces/Mayo.png", mayo.ImageUrl);
        Assert.Equal("Mayonnaise, often called mayo, is a thick, creamy condiment that is often used on sandwiches and salads. It is made from oil, egg yolks, and vinegar.", mayo.Description);
    }
}

public class BBQSauceTests
{
    [Fact]
    public void BBQSauce_IsDefined_True()
    {
        // Arrange & Act
        var bbqSauce = new BBQSauce();

        // Assert
        Assert.True(bbqSauce != null);
    }
    [Fact]
    public void BBQSauce_Ctor_SetsProperties()
    {
        // Arrange & Act
        var bbqSauce = new BBQSauce();

        // Assert
        Assert.Equal(4, bbqSauce.Id);
        Assert.Equal("BBQ Sauce", bbqSauce.Name);
        Assert.Equal(2.00m, bbqSauce.Price);
        Assert.Equal("../wwwroot/images/Sauces/BBQSauce.png", bbqSauce.ImageUrl);
        Assert.Equal("BBQ sauce is a tangy, sweet, and smoky condiment that is often used on grilled meats and sandwiches. It is made from tomatoes, vinegar, and spices.", bbqSauce.Description);
    }

}

public class HotSauceTests
{
    [Fact]
    public void HotSauce_IsDefined_True()
    {
        // Arrange & Act
        var hotSauce = new HotSauce();

        // Assert
        Assert.True(hotSauce != null);
    }
    [Fact]
    public void HotSauce_Ctor_SetsProperties()
    {
        // Arrange & Act
        var hotSauce = new HotSauce();

        // Assert
        Assert.Equal(5, hotSauce.Id);
        Assert.Equal("Hot Sauce", hotSauce.Name);
        Assert.Equal(2.00m, hotSauce.Price);
        Assert.Equal("../wwwroot/images/Sauces/HotSauce.png", hotSauce.ImageUrl);
        Assert.Equal("Hot sauce is a spicy condiment that is often used to add heat to dishes. It is made from chili peppers, vinegar, and spices.", hotSauce.Description);
    }
}

public class RanchTests
{
    [Fact]
    public void Ranch_IsDefined_True()
    {
        // Arrange & Act
        var ranch = new Ranch();

        // Assert
        Assert.True(ranch != null);
    }
    [Fact]
    public void Ranch_Ctor_SetsProperties()
    {
        // Arrange & Act
        var ranch = new Ranch();

        // Assert
        Assert.Equal(6, ranch.Id);
        Assert.Equal("Ranch", ranch.Name);
        Assert.Equal(2.00m, ranch.Price);
        Assert.Equal("../wwwroot/images/Sauces/Ranch.png", ranch.ImageUrl);
        Assert.Equal("Ranch dressing is a creamy, tangy condiment that is often used on salads and as a dipping sauce. It is made from buttermilk, mayonnaise, and herbs.", ranch.Description);
    }
}

public class AioliTests
{
    [Fact]
    public void Aioli_IsDefined_True()
    {
        // Arrange & Act
        var aioli = new Aioli();

        // Assert
        Assert.True(aioli != null);
    }
    [Fact]
    public void Aioli_Ctor_SetsProperties()
    {
        // Arrange & Act
        var aioli = new Aioli();

        // Assert
        Assert.Equal(7, aioli.Id);
        Assert.Equal("Aioli", aioli.Name);
        Assert.Equal(2.00m, aioli.Price);
        Assert.Equal("../wwwroot/images/Sauces/Aioli.png", aioli.ImageUrl);
        Assert.Equal("Aioli is a garlicky mayonnaise that is often used as a dipping sauce or spread. It is made from garlic, oil, and egg yolks.", aioli.Description);
    }
}

