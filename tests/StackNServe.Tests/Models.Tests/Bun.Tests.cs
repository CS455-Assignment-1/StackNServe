namespace StackNServe.Tests;

public class PlainBunTests
{
    [Fact]
    public void PlainBun_IsDefined_True()
    {
        // Arrange & Act
        var plainBun = new Plain_Bun();

        // Assert
        Assert.True(plainBun != null);
    }
    [Fact]
    public void PlainBun_Ctor_SetsProperties()
    {
        // Arrange & Act
        var plainBun = new Plain_Bun();

        // Assert
        Assert.Equal(1, plainBun.Id);
        Assert.Equal("Plain Bun", plainBun.Name);
        Assert.Equal(10.00m, plainBun.Price);
        Assert.Equal("../wwwroot/images/Bun/Plain_Bun.png", plainBun.ImageUrl);
        Assert.Equal("A plain bun is a type of sweet bread roll. They are usually dusted with flour and have a slightly sweet taste. They are often served with butter or jam.", plainBun.Description);
    }

}

public class SesameBunTests
{
    [Fact]
    public void SesameBun_IsDefined_True()
    {
        // Arrange & Act
        var sesameBun = new Sesame_Bun();

        // Assert
        Assert.True(sesameBun != null);
    }
    [Fact]
    public void SesameBun_Ctor_SetsProperties()
    {
        // Arrange & Act
        var sesameBun = new Sesame_Bun();

        // Assert
        Assert.Equal(2, sesameBun.Id);
        Assert.Equal("Sesame Bun", sesameBun.Name);
        Assert.Equal(12.00m, sesameBun.Price);
        Assert.Equal("../wwwroot/images/Bun/Sesame_Bun.png", sesameBun.ImageUrl);
        Assert.Equal("A sesame bun is a type of sweet bread roll. They are usually dusted with sesame seeds and have a slightly sweet taste.", sesameBun.Description);
    }

}

public class GarlicBunTests
{
    [Fact]
    public void GarlicBun_Ctor_IsDefined_True()
    {
        // Arrange & Act
        var garlicBun = new Garlic_Bun();

        // Assert
        Assert.True(garlicBun != null);
    }
    [Fact]
    public void GarlicBun_Ctor_SetsProperties()
    {
        // Arrange & Act
        var garlicBun = new Garlic_Bun();

        // Assert
        Assert.Equal(3, garlicBun.Id);
        Assert.Equal("Garlic Bun", garlicBun.Name);
        Assert.Equal(20.00m, garlicBun.Price);
        Assert.Equal("../wwwroot/images/Bun/Garlic_Bun.png", garlicBun.ImageUrl);
        Assert.Equal("A garlic bun is a type of savory bread roll. They are usually dusted with garlic and have a savory taste.", garlicBun.Description);
    }

}

public class ParmesanBunTests
{
    [Fact]
    public void ParmesanBun_IsDefined_True()
    {
        // Arrange & Act
        var parmesanBun = new Parmesan_Bun();

        // Assert
        Assert.True(parmesanBun != null);
    }
    [Fact]
    public void ParmesanBun_Ctor_SetsProperties()
    {
        // Arrange & Act
        var parmesanBun = new Parmesan_Bun();

        // Assert
        Assert.Equal(4, parmesanBun.Id);
        Assert.Equal("Parmesan Bun", parmesanBun.Name);
        Assert.Equal(25.00m, parmesanBun.Price);
        Assert.Equal("../wwwroot/images/Bun/Parmesan_Bun.png", parmesanBun.ImageUrl);
        Assert.Equal("A parmesan bun is a type of savory bread roll. They are usually dusted with parmesan cheese and have a savory taste.", parmesanBun.Description);
    }
}