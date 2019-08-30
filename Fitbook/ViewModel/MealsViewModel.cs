using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Models;

namespace Fitbook.ViewModel
{
    public class MealsViewModel
    {
        List<MealFood> MealsWithFood { get; set; }
        Day Day { get; set; }
    }
}
