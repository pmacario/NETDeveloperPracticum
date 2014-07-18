using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderFood
{
    public class Constants
    {
        public const string INSTRUCTIONS = "Please enter your order by specifying the time of day followed by a comma and\nthe number of the dish type.\n\nThe valid options for time of day are: \"morning\" or \"night\".\nThe valid options for dish type are: 1 (entrée), 2 (side), 3 (drink), \n\tor 4 (dessert).\n\nExample:\n\tmorning, 1, 2, 3\n\tnight, 1, 2, 3, 4\n\nPlease enter \"exit\" to close the application.";
        public const string INVALID_ORDER_SYNTAX_MESSAGE = "\n\nYou must enter a comma-delimited list of dish types with at least one valid\nselection. Please try again.\n";
        public const string INPUT_PREFIX = "Input: ";
        public const string OUTPUT_PREFIX = "Output: ";
        public const string ERROR_STRING = "error";
    }
}
