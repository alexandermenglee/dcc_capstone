using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitbook.Models
{
    public class MealFood
    {
        public int MealId { get; set; }
        public Meal Meal { get; set; }

        public int FoodId { get; set; }
        public Food Food { get; set; }
    }
}
