using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Models;

namespace Fitbook.ViewModel
{
    public class CompareFoodViewModel
    {
        public List<Food> Foods { get; set; }
        public int DayId { get; set; }
    }
}
