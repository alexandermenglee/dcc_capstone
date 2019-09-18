using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Models;

namespace Fitbook.Models
{
    public class Chat
    {
        [Key]
        public int ChatId { get; set; }
        public List<Message> Messages { get; set; }
        [NotMapped]
        public List<FitbookUser> FitbookUsers { get; set; } = new List<FitbookUser>();

        public List<UserChat> UserChat { get; set; } = new List<UserChat>();
    }
}
