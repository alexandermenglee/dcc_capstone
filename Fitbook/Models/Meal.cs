using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Models;

namespace Fitbook.Models
{
    public class Meal
    {
        [Key]
        public int MealId { get; set; }


        public List<MealFood> MealFoods { get; set; } = new List<MealFood>();
        public Day Day { get; set; }
    }
}
