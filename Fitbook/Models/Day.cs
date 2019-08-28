using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fitbook.Models
{
    public class Day
    {
        [Key]
        public int DayId { get; set; }
        DateTime day { get; set; }
        public List<Food> Foods { get; set; }
        public int Meals { get; set; }
        FitbookUsersMacronutrients Nutrition { get; set; }
        public int FitbookUserId { get; set; }
        [ForeignKey("FitbookUserId")]
        public FitbookUser FitbookUser { get; set; }
    }
}
