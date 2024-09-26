namespace StackNServe.Models
{
    public class Plain_Bun
    {
        public Plain_Bun()
        {
            this.Id = 1;
            this.Name = "Plain Bun";
            this.Price = 10.00m;
            this.ImageUrl = "../wwwroot/images/Bun/Plain_Bun.png";
            this.Description = "A plain bun is a type of sweet bread roll. They are usually dusted with flour and have a slightly sweet taste. They are often served with butter or jam.";
        }

        /// <summary>
        /// Gets or sets the unique identifier for the bun.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the bun.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the bun.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the URL of the image for the bun.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the description of the bun.
        /// </summary>
        public string Description { get; set; }
    }

    public class Sesame_Bun
    {
        public Sesame_Bun()
        {
            this.Id = 2;
            this.Name = "Sesame Bun";
            this.Price = 12.00m;
            this.ImageUrl = "../wwwroot/images/Bun/Sesame_Bun.png";
            this.Description = "A sesame bun is a type of sweet bread roll. They are usually dusted with sesame seeds and have a slightly sweet taste.";
        }

        /// <summary>
        /// Gets or sets the unique identifier for the bun.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the bun.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the bun.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the URL of the image for the bun.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the description of the bun.
        /// </summary>
        public string Description { get; set; }
    }

    public class Garlic_Bun
    {
        public Garlic_Bun()
        {
            this.Id = 3;
            this.Name = "Garlic Bun";
            this.Price = 20.00m;
            this.ImageUrl = "../wwwroot/images/Bun/Garlic_Bun.png";
            this.Description = "A garlic bun is a type of savory bread roll. They are usually dusted with garlic and have a savory taste.";
        }

        /// <summary>
        /// Gets or sets the unique identifier for the bun.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the bun.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the bun.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the URL of the image for the bun.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the description of the bun.
        /// </summary>
        public string Description { get; set; }
    }

    public class Parmesan_Bun
    {
        public Parmesan_Bun()
        {
            this.Id = 4;
            this.Name = "Parmesan Bun";
            this.Price = 25.00m;
            this.ImageUrl = "../wwwroot/images/Bun/Parmesan_Bun.png";
            this.Description = "A parmesan bun is a type of savory bread roll. They are usually dusted with parmesan cheese and have a savory taste.";
        }

        /// <summary>
        /// Gets or sets the unique identifier for the bun.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the bun.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the bun.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the URL of the image for the bun.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the description of the bun.
        /// </summary>
        public string Description { get; set; }
    }

}
