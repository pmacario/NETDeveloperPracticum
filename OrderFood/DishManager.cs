using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderFood
{
    public class DishManager
    {
        readonly List<Dish> _dishes;

        private const string COMMA_SPACE = ", ";
        private const string DISH_COUNT_FORMAT = "(x{0})";

        public DishManager(List<Dish> dishMenu)
        {
            _dishes = dishMenu;
        }

        public string ReturnDishes(string order)
        {
            // Initialize the output string with "Output: "
            StringBuilder output = new StringBuilder(Constants.OUTPUT_PREFIX);

            // The order should come in as a comma delimited list.
            // So we split the string by the delimiter.
            string[] orderComponents = order.Split(',');

            // The order must include at least 2 elements: a time of day 
            // and at least one dish.
            if (orderComponents.Length < 2)
            {
                AddErrorToOutput(output);
            }
            else
            {
                // Trim any extra whitespace from the specified time of day.
                string timeOfDay = orderComponents[0].Trim().ToLower();
                string[] dishRequests = orderComponents.Skip(1).ToArray();

                // Check that the specified time of day is one of the valid
                // expected options: "morning" or "night".
                TimeOfDay convertedTimeOfDay;
                if (Enum.TryParse(timeOfDay, true, out convertedTimeOfDay))
                {
                    // Grab all the dishes for the specified time of day.
                    List<Dish> dishesForTimeOfDay = _dishes.Where(dish => dish.TimeOfDay == convertedTimeOfDay).ToList();

                    List<DishType> sortedDishRequests = new List<DishType>();

                    // Build the dish request list.
                    foreach (string dishRequest in dishRequests)
                    {
                        // Trim any extra whitespace from the dish request.
                        string cleanDishRequest = dishRequest.Trim();

                        // Check that the dish request is an integer and that
                        // it corresponds to a valid dish type:
                        // 1 (entrée), 2 (side), 3 (drink), or 4 (dessert).
                        int result;
                        if (int.TryParse(cleanDishRequest, out result) && Enum.IsDefined(typeof(DishType), result)
                                && (DishType)result != DishType.InvalidDishType)
                        {
                            sortedDishRequests.Add((DishType)result);
                        }
                        else
                        {
                            // An invalid selection is encountered.
                            // So we add the InvalidDishType to the dish
                            // request list and stop processing the order.
                            sortedDishRequests.Add(DishType.InvalidDishType);
                            break;
                        }
                    }

                    // Sort the dish request list so that we can print the food in the
                    // following order: entrée, side, drink, dessert.
                    sortedDishRequests.Sort();

                    List<Dish> orderedDishes = new List<Dish>();

                    foreach (DishType dishRequest in sortedDishRequests)
                    {
                        // Check if the dish request is valid given the time of day 
                        // and the rest of the dishes already ordered.
                        if (IsValidDishRequest(dishRequest, convertedTimeOfDay, orderedDishes, dishesForTimeOfDay))
                        {
                            // Grab the requested dish from the dishes available for the time of day.
                            Dish requestedDish = dishesForTimeOfDay.Find(dish => dish.DishType == dishRequest);

                            // Check if the dish has already been ordered.
                            if (!orderedDishes.Exists(dish => dish.DishType == dishRequest))
                            {
                                // This is the first occurrence of this dish in the order.
                                // So we add it to the list of ordered dishes and add its
                                // name to the output.
                                orderedDishes.Add(requestedDish);
                                AddDishNameToOutput(output, requestedDish.DishName);
                            }
                            else
                            {
                                // This is a repeat occurrence of this dish in the order.
                                // So the name of the dish has already been added to the output.
                                // Therefore, we find the total count of this dish in the order
                                // and adjust the output to reflect that total.
                                int dishCount = sortedDishRequests.Count(i => dishRequest == i);
                                AddDishCountToOutput(output, requestedDish.DishName, dishCount);
                            }
                        }
                        else
                        {
                            // An invalid selection is encountered.
                            // So we add "error" to the output and stop processing
                            // the rest of the order.
                            AddErrorToOutput(output);
                            break;
                        }
                    }
                }
                else
                    // An invalid time of day is specified.
                    // So we add "error" to the output and stop processing
                    // the rest of the order.
                    AddErrorToOutput(output);
            }

            return output.ToString();
        }

        /// <summary>
        /// Adds the dish name to the output, pre-pending a comma and a space if
        /// the output already contains other dishes.
        /// </summary>
        /// <param name="output">the StringBuilder object to manipulate</param>
        /// <param name="dishName">the dish name</param>
        private static void AddDishNameToOutput(StringBuilder output, string dishName)
        {
            if (output.ToString() != Constants.OUTPUT_PREFIX)
                output.Append(COMMA_SPACE);

            output.Append(dishName);
        }

        /// <summary>
        /// Adds the multiplier count "(xSomeNumber)" to the output when there are
        /// multiple orders of the same dish.
        /// </summary>
        /// <param name="output">the StringBuilder object to manipulate</param>
        /// <param name="dishName">the dish name</param>
        /// <param name="dishCount">the total count of the dish for the order</param>
        private static void AddDishCountToOutput(StringBuilder output, string dishName, int dishCount)
        {
            string interimOutput = output.ToString();
            int startingIndex = interimOutput.LastIndexOf(dishName) + dishName.Length;
            
            if (startingIndex == output.Length)
                // Only add the dish count to the output if it has
                // not already been added previously.
                output.AppendFormat(DISH_COUNT_FORMAT, dishCount);
        }

        /// <summary>
        /// Adds "error" to the output, pre-pending a comma and a space if
        /// the output already contains dishes.
        /// </summary>
        /// <param name="output">the StringBuilder object to manipulate</param>
        private static void AddErrorToOutput(StringBuilder output)
        {
            if (output.ToString() != Constants.OUTPUT_PREFIX)
                output.Append(COMMA_SPACE);

            output.Append(Constants.ERROR_STRING);
        }

        /// <summary>
        /// Checks whether the specified dish request is valid for the specified
        /// time of day, given the full order.
        /// </summary>
        /// <param name="dishRequest">the requested dish as a DishType enum</param>
        /// <param name="timeOfDay">the specified time of day as a TimeOfDay enum</param>
        /// <param name="dishesAlreadyOrdered">all the dishes already ordered</param>
        /// <param name="dishesForTimeOfDay">the list of dishes available for the time of day</param>
        /// <returns>boolean indicating whether the dish request is valid</returns>
        private bool IsValidDishRequest(DishType dishRequest, TimeOfDay timeOfDay, List<Dish> dishesAlreadyOrdered, List<Dish> dishesForTimeOfDay)
        {
            // Check for invalid dish selection
            if (dishRequest == DishType.InvalidDishType)
                return false;

            // Check whether the requested DishType is available for the specified
            // time of day.
            if (!dishesForTimeOfDay.Exists(dish => dish.DishType == dishRequest))
                return false;

            if (timeOfDay == TimeOfDay.Morning)
            {
                // In the morning, you can order multiple cups of coffee (DishType = Drink).
                if (dishRequest == DishType.Drink)
                    return true;
                // For all other dish types, you can have at most 1 of each dish type per order.
                else
                    return !dishesAlreadyOrdered.Exists(dish => dish.DishType == dishRequest);
            }
            else if (timeOfDay == TimeOfDay.Night)
            {
                // At night, you can have multiple orders of potatoes (DishType = Side).
                if (dishRequest == DishType.Side)
                    return true;
                // For all other dish types, you can have at most 1 of each dish type per order.
                else
                    return !dishesAlreadyOrdered.Exists(dish => dish.DishType == dishRequest);
            }

            return false;
        }
    }
}
