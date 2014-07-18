using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OrderFood
{
    public struct Dish
    {
        public TimeOfDay TimeOfDay { get; set; }
        public DishType DishType { get; set; }
        public string DishName { get; set; }
    }

    public enum DishType
    {
        Entree = 1,
        Side = 2,
        Drink = 3,
        Dessert = 4,
        InvalidDishType = int.MaxValue
    }

    public enum TimeOfDay
    {
        [Description("morning")]
        Morning,
        [Description("night")]
        Night
    }
}
