using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Components.WebAssembly.Services;

namespace StackNServe
{
    public class BurgerComponents
    {
        Dictionary<int, string> Bun= new Dictionary<int, string>();
        Dictionary<int, string> Patty= new Dictionary<int, string>();
        Dictionary<int, string> Toppings= new Dictionary<int, string>();
        Dictionary<int, string> Sauces= new Dictionary<int, string>();
        public List<string> Order_List = new List<string>();

        public BurgerComponents()
        {
            InitializeBuns();
            InitializePatties();
            InitializeToppings();
            InitializeSauces();

            int Bun_Type = GetSecureRandomNumber(1, 5); // Select a bun type
            int Num_Patty = GetSecureRandomNumber(1, 5); // Number of patties
            HashSet<int> Patty_Type = new HashSet<int>();
            while (Patty_Type.Count < Num_Patty)
            {
                Patty_Type.Add(GetSecureRandomNumber(1, Patty.Count + 1)); // Select patty types
            }

            int Num_Topping = GetSecureRandomNumber(1, 5); // Number of toppings
            HashSet<int> Topping_Type = new HashSet<int>();
            while (Topping_Type.Count < Num_Topping)
            {
                Topping_Type.Add(GetSecureRandomNumber(1, Toppings.Count + 1)); // Select toppings
            }

            int Num_Sauce = GetSecureRandomNumber(1, 5); // Number of sauces
            HashSet<int> Sauce_Type = new HashSet<int>();
            while (Sauce_Type.Count < Num_Sauce)
            {
                Sauce_Type.Add(GetSecureRandomNumber(1, Sauces.Count + 1)); // Select sauces
            }
            
            // make the order list
            Order_List.Add(Bun[Bun_Type]);
            foreach (int patty in Patty_Type)
            {
                Order_List.Add(Patty[patty]);
            }
            foreach (int topping in Topping_Type)
            {
                Order_List.Add(Toppings[topping]);
            }
            foreach (int sauce in Sauce_Type)
            {
                Order_List.Add(Sauces[sauce]);
            }
            // randomize the list
            Order_List = RandomizeList(Order_List);

            // console log the list
            foreach (string item in Order_List)
            {
                Console.WriteLine(item);
            }
        }

        public int GetSecureRandomNumber(int min, int max)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] data = new byte[4];
                rng.GetBytes(data);
                int value = BitConverter.ToInt32(data, 0) & int.MaxValue; // Ensure it's non-negative
                return new Random(value).Next(min, max);
            }
        }

        private List<string> RandomizeList(List<string> list)
        {
            List<string> randomizedList = new List<string>(list);
            for (int i = 0; i < randomizedList.Count; i++)
            {
                int j = GetSecureRandomNumber(0, randomizedList.Count); // Use secure random numbers
                var temp = randomizedList[i];
                randomizedList[i] = randomizedList[j];
                randomizedList[j] = temp;
            }
            return randomizedList;
        }
        private void InitializeBuns()
        {
            Bun.Add(1, "Plain Bun");
            Bun.Add(2, "Sesame Bun");
            Bun.Add(3, "Garlic Bun");
            Bun.Add(4, "Parmesan Bun");
        }
        private void InitializePatties()
        {
            Patty.Add(1, "Chicken Patty");
            Patty.Add(2, "Veggie Patty");
            Patty.Add(3, "Fish Patty");
            Patty.Add(4, "Portobello Mushroom Patty");
        }

        private void InitializeToppings()
        {
            Toppings.Add(1, "Tomato");
            Toppings.Add(2, "Lettuce");
            Toppings.Add(3, "Onion");
            Toppings.Add(4, "Pickles");
            Toppings.Add(5, "Jalapenos");
            Toppings.Add(6, "Avocado");
            Toppings.Add(7, "Bacon");
            Toppings.Add(8, "Cheese");
            Toppings.Add(9, "Egg");
        }

        private void InitializeSauces()
        {
            Sauces.Add(1, "Mustard");
            Sauces.Add(2, "Ketchup");
            Sauces.Add(3, "Mayonnaise");
            Sauces.Add(4, "BBQ Sauce");
            Sauces.Add(5, "Hot Sauce");
            Sauces.Add(6, "Ranch");
            Sauces.Add(7, "Aioli");
        }
    }

}
