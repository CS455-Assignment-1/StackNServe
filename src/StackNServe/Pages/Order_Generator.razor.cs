using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.WebAssembly.Services;

using StackNServe.Models;
using StackNServe.Services;

namespace StackNServe
{
    public class BurgerComponents
    {
        readonly Dictionary<int, string> Bun = new Dictionary<int, string>();
        readonly Dictionary<int, string> Patty = new Dictionary<int, string>();
        readonly Dictionary<int, string> Toppings = new Dictionary<int, string>();
        readonly Dictionary<int, string> Sauces = new Dictionary<int, string>();
        public List<string> Order_List = new List<string>();

        public BurgerComponents()
        {
            InitializeBuns();
            InitializePatties();
            InitializeToppings();
            InitializeSauces();

            int bunType = GetSecureRandomNumber(1, 5); // Select a bun type
            int numPatty = GetSecureRandomNumber(1, 5); // Number of patties
            HashSet<int> pattyType = new HashSet<int>();
            while (pattyType.Count < numPatty)
            {
                pattyType.Add(GetSecureRandomNumber(1, Patty.Count + 1)); // Select patty types
            }

            int numTopping = GetSecureRandomNumber(1, 5); // Number of toppings
            HashSet<int> toppingType = new HashSet<int>();
            while (toppingType.Count < numTopping)
            {
                toppingType.Add(GetSecureRandomNumber(1, Toppings.Count + 1)); // Select toppings
            }

            int numSauce = GetSecureRandomNumber(1, 5); // Number of sauces
            HashSet<int> sauceType = new HashSet<int>();
            while (sauceType.Count < numSauce)
            {
                sauceType.Add(GetSecureRandomNumber(1, Sauces.Count + 1)); // Select sauces
            }
            // Make the order list
            Order_List.Add(Bun[bunType]);
            foreach (int patty in pattyType)
            {
                Order_List.Add(Patty[patty]);
            }
            foreach (int topping in toppingType)
            {
                Order_List.Add(Toppings[topping]);
            }
            foreach (int sauce in sauceType)
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
                int value = BitConverter.ToInt32(data, 0) & int.MaxValue;
                return value % (max - min) + min;
            }
        }

        public List<string> RandomizeList(List<string> list)
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
