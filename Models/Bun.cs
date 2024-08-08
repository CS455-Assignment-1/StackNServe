public class Plain_Bun
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public Plain_Bun()
    {
        Id = 1;
        Name = "Plain Bun";
        Price = 10.00m;
        ImageUrl = "../wwwroot/images/Bun/Plain_Bun.png";
        Description = "A plain bun is a type of sweet bread roll. They are usually dusted with flour and have a slightly sweet taste. They are often served with butter or jam.";
    }
}

public class Sesame_Bun
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public Sesame_Bun()
    {
        Id = 2;
        Name = "Sesame Bun";
        Price = 12.00m;
        ImageUrl = "../wwwroot/images/Bun/Sesame_Bun.png";
        Description = "A sesame bun is a type of sweet bread roll. They are usually dusted with sesame seeds and have a slightly sweet taste.";
    }
}

public class Garlic_Bun
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public Garlic_Bun()
    {
        Id = 3;
        Name = "Garlic Bun";
        Price = 20.00m;
        ImageUrl = "../wwwroot/images/Bun/Garlic_Bun.png";
        Description = "A garlic bun is a type of savory bread roll. They are usually dusted with garlic and have a savory taste.";
    }
}

public class Parmesan_Bun
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public Parmesan_Bun()
    {
        Id = 4;
        Name = "Parmesan Bun";
        Price = 25.00m;
        ImageUrl = "../wwwroot/images/Bun/Parmesan_Bun.png";
        Description = "A parmesan bun is a type of savory bread roll. They are usually dusted with parmesan cheese and have a savory taste.";
    }
}