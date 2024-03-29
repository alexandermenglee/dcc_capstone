﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Models;

namespace Fitbook.ViewModel
{
    public class IndexViewModel
    {
        public FitbookUser fitbookUser { get; set; }
        public FitbookUsersMacronutrients fitbookUsersMacronutrients { get; set; }
        public List<Meal> Meals { get; set; } = new List<Meal>();
        public Day Day { get; set; }
    }
}
