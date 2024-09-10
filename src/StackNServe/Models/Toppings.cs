namespace StackNServe
{
    public class Tomato
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public Tomato()
        {
            Id = 1;
            Name = "Tomato";
            Price = 5.00m;
            ImageUrl = "../wwwroot/images/Toppings/Tomato.png";
            Description = "A tomato is a red, round fruit that is often used as a vegetable in cooking. They are rich in vitamins and minerals.";
        }
    }

    public class Lettuce
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public Lettuce()
        {
            Id = 2;
            Name = "Lettuce";
            Price = 3.00m;
            ImageUrl = "../wwwroot/images/Toppings/Lettuce.png";
            Description = "Lettuce is a leafy green vegetable that is often used in salads and sandwiches. It is rich in vitamins and minerals.";
        }
    }

    public class Onion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public Onion()
        {
            Id = 3;
            Name = "Onion";
            Price = 4.00m;
            ImageUrl = "../wwwroot/images/Toppings/Onion.png";
            Description = "An onion is a round, bulbous vegetable that is often used in cooking. They are rich in vitamins and minerals.";
        }
    }

    public class Pickles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public Pickles()
        {
            Id = 4;
            Name = "Pickles";
            Price = 2.00m;
            ImageUrl = "../wwwroot/images/Toppings/Pickles.png";
            Description = "Pickles are cucumbers that have been pickled in a brine of vinegar, salt, and spices. They are often served as a condiment.";
        }
    }

    public class Jalapenos
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public Jalapenos()
        {
            Id = 5;
            Name = "Jalapenos";
            Price = 6.00m;
            ImageUrl = "../wwwroot/images/Toppings/Jalapenos.png";
            Description = "Jalapenos are a type of chili pepper that is often used in Mexican cuisine. They are known for their spicy flavor.";
        }
    }

    public class Avocado
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public Avocado()
        {
            Id = 6;
            Name = "Avocado";
            Price = 8.00m;
            ImageUrl = "../wwwroot/images/Toppings/Avocado.png";
            Description = "An avocado is a green, pear-shaped fruit that is often used in salads and sandwiches. They are rich in healthy fats and vitamins.";
        }
    }

    public class Bacon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public Bacon()
        {
            Id = 7;
            Name = "Bacon";
            Price = 7.00m;
            ImageUrl = "../wwwroot/images/Toppings/Bacon.png";
            Description = "Bacon is a type of salt-cured pork that is often used in cooking. It is known for its rich, smoky flavor.";
        }
    }

    public class Cheese
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public Cheese()
        {
            Id = 8;
            Name = "Cheese";
            Price = 5.00m;
            ImageUrl = "../wwwroot/images/Toppings/Cheese.png";
            Description = "Cheese is a dairy product that is often used in cooking. It is known for its rich, creamy flavor.";
        }
    }

    public class Egg
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public Egg()
        {
            Id = 9;
            Name = "Egg";
            Price = 4.00m;
            ImageUrl = "../wwwroot/images/Toppings/Egg.png";
            Description = "An egg is a type of food that is often used in cooking. They are rich in protein and vitamins.";
        }
    }
}