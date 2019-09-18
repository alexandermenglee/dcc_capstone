using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Models;

namespace Fitbook.ViewModel
{
    public class CustomRecipeIndexViewModel
    {
        public List<CustomRecipe> customRecipes { get; set; }
        public List<FitbookUser> fitbookUsers { get; set; }
        public List<Category> Categories { get; set; }
    }
}
