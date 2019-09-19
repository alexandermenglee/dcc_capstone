using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fitbook.Data;
using Fitbook.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Fitbook.Controllers
{
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MessageController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Message message)
        {
            string currentAppUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            FitbookUser currentUserId = _context.FitbookUsers.Where(f => f.ApplicationUserId.Equals(currentAppUserId)).Single();
            Message newMessage = new Message()
            {
                
            };
             
        }
    }
}
