namespace StackNServe.Models
{
    public class Chicken_Patty
    {
        public Chicken_Patty()
        {
            this.Id = 1;
            this.Name = "Chicken Patty";
            this.Price = 20.00m;
            this.ImageUrl = "../wwwroot/images/Patty/Chicken_Patty.png";
            this.Description = "A chicken patty is a type of breaded chicken cutlet. They are usually served with lettuce, tomato, and mayonnaise.";
        }

        /// <summary>
        /// Gets or sets the unique identifier for the patty.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the patty.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the patty.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the URL of the image for the patty.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the description of the patty.
        /// </summary>
        public string Description { get; set; }
    }

    public class Veggie_Patty
    {
        public Veggie_Patty()
        {
            this.Id = 2;
            this.Name = "Veggie Patty";
            this.Price = 15.00m;
            this.ImageUrl = "../wwwroot/images/Patty/Veggie_Patty.png";
            this.Description = "A veggie patty is as nutritious as it is delicious. They can be made from a variety of vegetables, grains, and legumes.";

        }

        /// <summary>
        /// Gets or sets the unique identifier for the patty.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the patty.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the patty.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the URL of the image for the patty.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the description of the patty.
        /// </summary>
        public string Description { get; set; }
    }

    public class Fish_Patty
    {
        public Fish_Patty()
        {
            this.Id = 3;
            this.Name = "Fish Patty";
            this.Price = 25.00m;
            this.ImageUrl = "../wwwroot/images/Patty/Fish_Patty.png";
            this.Description = "A fish patty is a unique blend of fish, spices, and herbs. They are usually served with tartar sauce and a slice of lemon.";
        }

        /// <summary>
        /// Gets or sets the unique identifier for the patty.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the patty.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the patty.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the URL of the image for the patty.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the description of the patty.
        /// </summary>
        public string Description { get; set; }
    }

    public class Portobello_Mushroom_Patty
    {
        public Portobello_Mushroom_Patty()
        {
            this.Id = 4;
            this.Name = "Portobello Mushroom Patty";
            this.Price = 18.00m;
            this.ImageUrl = "../wwwroot/images/Patty/Portobello_Mushroom_Patty.png";
            this.Description = "A portobello mushroom burger is a vegetarian burger made from a large, mature portobello mushroom. They are usually marinated and grilled to perfection.";
        }

        /// <summary>
        /// Gets or sets the unique identifier for the patty.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the patty.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the patty.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the URL of the image for the patty.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the description of the patty.
        /// </summary>
        public string Description { get; set; }
    }

}