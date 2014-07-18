using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace OrderFood.UnitTests
{
    [TestFixture]
    public class UnitTests
    {
        private DishManager _dishManager;

        [TestFixtureSetUp]
        public void Init()
        {
            _dishManager = new DishManager(
                new List<Dish>()
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
                }
            );
        }

        [TestCase("afternoon,1,2,3", Result = "Output: error")] // invalid time of day
        [TestCase("morning,1, 2, 3, 4", Result = "Output: eggs, toast, coffee, error")] // no dessert for morning meals 
        [TestCase("MoRnIng,1,2,3", Result = "Output: eggs, toast, coffee")] // case insensitivity check
        [TestCase("morning,2,1,3", Result = "Output: eggs, toast, coffee")] // dish type order check
        [TestCase("night,4,3,2,1", Result = "Output: steak, potato, wine, cake")] // dish type order check
        [TestCase("NIGHT,1,2,3,4", Result = "Output: steak, potato, wine, cake")] // case insensitivity check
        [TestCase("morning,1,3,3,3,3,2", Result = "Output: eggs, toast, coffee(x4)")] // valid multiple coffee order
        [TestCase("night,2,4,2,1", Result = "Output: steak, potato(x2), cake")] // valid multiple potato order
        [TestCase("night, 1, 2, 3, 5", Result = "Output: steak, potato, wine, error")] // invalid 4th dish selection (invalid dish type "5" specified)
        [TestCase("night, 1, 1, 2, 3, 5", Result = "Output: steak, error")] // invalid 2nd dish selection (only 1 entree allowed)
        [TestCase("", Result = "Output: error")] // no input entered
        [TestCase("night", Result = "Output: error")] // no dishes ordered
        public string Test_OrderDishes(string order)
        {
            return _dishManager.ReturnDishes(order);
        }
    }
}
