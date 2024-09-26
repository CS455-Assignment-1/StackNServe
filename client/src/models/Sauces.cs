namespace StackNServe.Models
{
    public class Mustard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public Mustard()
        {
            Id = 1;
            Name = "Mustard";
            Price = 2.00m;
            ImageUrl = "../wwwroot/images/Sauces/Mustard.png";
            Description = "Mustard is a condiment that is often used on sandwiches and hot dogs. It is made from the seeds of the mustard plant.";
        }
    }

    public class Ketchup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public Ketchup()
        {
            Id = 2;
            Name = "Ketchup";
            Price = 2.00m;
            ImageUrl = "../wwwroot/images/Sauces/Ketchup.png";
            Description = "Ketchup is a sweet and tangy condiment that is often used on burgers and fries. It is made from tomatoes, vinegar, and sugar.";
        }
    }

    public class Mayo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public Mayo()
        {
            Id = 3;
            Name = "Mayo";
            Price = 2.00m;
            ImageUrl = "../wwwroot/images/Sauces/Mayo.png";
            Description = "Mayonnaise, often called mayo, is a thick, creamy condiment that is often used on sandwiches and salads. It is made from oil, egg yolks, and vinegar.";
        }
    }

    public class BBQSauce
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public BBQSauce()
        {
            Id = 4;
            Name = "BBQ Sauce";
            Price = 2.00m;
            ImageUrl = "../wwwroot/images/Sauces/BBQSauce.png";
            Description = "BBQ sauce is a tangy, sweet, and smoky condiment that is often used on grilled meats and sandwiches. It is made from tomatoes, vinegar, and spices.";
        }
    }

    public class HotSauce
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public HotSauce()
        {
            Id = 5;
            Name = "Hot Sauce";
            Price = 2.00m;
            ImageUrl = "../wwwroot/images/Sauces/HotSauce.png";
            Description = "Hot sauce is a spicy condiment that is often used to add heat to dishes. It is made from chili peppers, vinegar, and spices.";
        }
    }

    public class Ranch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public Ranch()
        {
            Id = 6;
            Name = "Ranch";
            Price = 2.00m;
            ImageUrl = "../wwwroot/images/Sauces/Ranch.png";
            Description = "Ranch dressing is a creamy, tangy condiment that is often used on salads and as a dipping sauce. It is made from buttermilk, mayonnaise, and herbs.";
        }
    }

    public class Aioli
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public Aioli()
        {
            Id = 7;
            Name = "Aioli";
            Price = 2.00m;
            ImageUrl = "../wwwroot/images/Sauces/Aioli.png";
            Description = "Aioli is a garlicky mayonnaise that is often used as a dipping sauce or spread. It is made from garlic, oil, and egg yolks.";
        }
    }

}
