using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitbook.Models
{
    public class UserChat
    {
        public int FitbookUserId { get; set; }
        public FitbookUser FitbookUser { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
