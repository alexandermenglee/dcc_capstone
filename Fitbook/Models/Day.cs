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
        public DateTime Date { get; set; }

        // List of Meals
        public List<Meal> Meals { get; set; } = new List<Meal>();

        // foreign key for FitbookUser
        public int FitbookUserId { get; set; }
        [ForeignKey("FitbookUserId")]
        public FitbookUser FitbookUser { get; set; }
    }
}
// post meals to controller
// create that many meal objects 
// each meal contains food objects