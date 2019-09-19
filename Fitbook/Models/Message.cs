using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fitbook.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string MessaegText { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        // Foregin Key to Chat object
        public int ChatId{ get; set; }
        [ForeignKey("ChatId")]
        public Chat Chat{ get; set; }
    }
}
