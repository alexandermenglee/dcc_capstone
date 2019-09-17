using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Models;

namespace Fitbook.ViewModel
{
    public class AddRecipeViewModel
    {
        public List<Category> Categories { get; set; }
        public CustomRecipe CustomRecipe { get; set; }
    }
}
