using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fitbook.Models
{
    public class FitbookUser
    {
        [Key]
        public int FitbookUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double? Age { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public int Meals { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
