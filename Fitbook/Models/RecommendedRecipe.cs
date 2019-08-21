using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fitbook.Models
{
    public class RecommendedRecipe
    {
        [Key]
        public int RecommendedRecipeId { get; set; }
    }
}
