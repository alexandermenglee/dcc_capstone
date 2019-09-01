using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Models;

namespace Fitbook.ViewModel
{
    public class IndexViewModel
    {
        public List<Meal> MealsWithFood { get; set; }
        public Day Day { get; set; }
    }
}
