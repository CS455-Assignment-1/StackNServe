using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Components.WebAssembly.Services;


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

        // select the ingredients
        Random random = new Random();
        int Bun_Type = random.Next(1, 5);
        Random Num_Patties = new Random();
        int Num_Patty = Num_Patties.Next(1, 5);
        HashSet<int> Patty_Type = new HashSet<int>();
        while (Patty_Type.Count < Num_Patty)
        {
            Patty_Type.Add(random.Next(1, Patty.Count + 1));
        }
        Random Num_Toppings = new Random();
        int Num_Topping = Num_Toppings.Next(1, 5);
        HashSet<int> Topping_Type = new HashSet<int>();
        while (Topping_Type.Count < Num_Topping)
        {
            Topping_Type.Add(random.Next(1, Toppings.Count + 1));
        }
        Random Num_Sauces = new Random();
        int Num_Sauce = Num_Sauces.Next(1, 5);
        HashSet<int> Sauce_Type = new HashSet<int>();
        while (Sauce_Type.Count < Num_Sauce)
        {
            Sauce_Type.Add(random.Next(1, Sauces.Count + 1));
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
        Order_List = Order_List.OrderBy(x => random.Next()).ToList();

        // console log the list
        foreach (string item in Order_List)
        {
            Console.WriteLine(item);
        }
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
