using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderFood
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Dish> dishMenu = new List<Dish>()
            {
                new Dish(){
                    TimeOfDay = TimeOfDay.Morning,
                    DishType = DishType.Entree,
                    DishName = "eggs"
                },
                new Dish(){
                    TimeOfDay = TimeOfDay.Morning,
                    DishType = DishType.Side,
                    DishName = "toast"
                },
                new Dish(){
                    TimeOfDay = TimeOfDay.Morning,
                    DishType = DishType.Drink,
                    DishName = "coffee"
                },
                new Dish(){
                    TimeOfDay = TimeOfDay.Night,
                    DishType = DishType.Entree,
                    DishName = "steak"
                },
                new Dish(){
                    TimeOfDay = TimeOfDay.Night,
                    DishType = DishType.Side,
                    DishName = "potato"
                },
                new Dish(){
                    TimeOfDay = TimeOfDay.Night,
                    DishType = DishType.Drink,
                    DishName = "wine"
                },
                new Dish(){
                    TimeOfDay = TimeOfDay.Night,
                    DishType = DishType.Dessert,
                    DishName = "cake"
                },
            };

            Console.WriteLine(Constants.INSTRUCTIONS);
            Console.WriteLine();
            Console.Write(Constants.INPUT_PREFIX);

            DishManager dishManager = new DishManager(dishMenu);
            string input;
            string output;
            while ((input = Console.ReadLine()) != "exit")
            {
                output = dishManager.ReturnDishes(input);

                Console.Write(output);

                if (output == string.Concat(Constants.OUTPUT_PREFIX, Constants.ERROR_STRING))
                {
                    Console.WriteLine(Constants.INVALID_ORDER_SYNTAX_MESSAGE);
                    Console.Write(Constants.INSTRUCTIONS);
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.Write(Constants.INPUT_PREFIX);
            }
        }
    }
}
