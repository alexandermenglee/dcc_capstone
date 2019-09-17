using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fitbook.Models
{
    public class CustomRecipe
    {
        [Key]
        public int CustomRecipeId { get; set; }
        public string CustomRecipeName { get; set; }
        [Column(TypeName = "varchar(500)")]
        public string Description { get; set; }
        public int Carbohydrates { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Calories { get; set; }
        public double? Servings { get; set; }
        public int CaloriesPerServing { get; set; }

        // Foreign Keys
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public int FitbookUserId { get; set; }
        [ForeignKey("FitbookUserId")]
        public FitbookUser FitbookUser { get; set; }
    }
}
