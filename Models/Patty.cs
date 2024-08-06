public class Chicken_Patty
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public Chicken_Patty()
    {
        Id = 1;
        Name = "Chicken Patty";
        Price = 20.00m;
        ImageUrl = "../wwwroot/images/Patty/Chicken_Patty.png";
        Description = "A chicken patty is a type of breaded chicken cutlet. They are usually served with lettuce, tomato, and mayonnaise.";
    }
}

public class Veggie_Patty
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public Veggie_Patty()
    {
        Id = 2;
        Name = "Veggie Patty";
        Price = 15.00m;
        ImageUrl = "../wwwroot/images/Patty/Veggie_Patty.png";
        Description = "A veggie patty is as nutritious as it is delicious. They can be made from a variety of vegetables, grains, and legumes.";

    }
}

public class Fish_Patty
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public Fish_Patty()
    {
        Id = 3;
        Name = "Fish Patty";
        Price = 25.00m;
        ImageUrl = "../wwwroot/images/Patty/Fish_Patty.png";
        Description = "A fish patty is a unique blend of fish, spices, and herbs. They are usually served with tartar sauce and a slice of lemon.";
    }
}

public class Portobello_Mushroom_Patty
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public Portobello_Mushroom_Burger()
    {
        Id = 4;
        Name = "Portobello Mushroom Patty";
        Price = 18.00m;
        ImageUrl = "../wwwroot/images/Patty/Portobello_Mushroom_Patty.png";
        Description = "A portobello mushroom burger is a vegetarian burger made from a large, mature portobello mushroom. They are usually marinated and grilled to perfection.";
    }
}