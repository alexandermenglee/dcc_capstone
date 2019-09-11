using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Models;

namespace Fitbook.ViewModel
{
    public class FoodQueryResultsViewModel
    {
        public List<Food> queryResults { get; set; } = new List<Food>();
        public int MealId { get; set; }
    }
}
