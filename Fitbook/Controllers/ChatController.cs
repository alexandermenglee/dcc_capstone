using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fitbook.Data;
using Fitbook.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fitbook.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ChatController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateChat()
        {
            List<FitbookUser> fitbookUsers = new List<FitbookUser>();
            List<int> fitbookUserIds = new List<int>();
            string currentAppUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            FitbookUser fitbookUser = _context.FitbookUsers.Include("UserChat").Where(f => f.ApplicationUserId.Equals(currentAppUserId)).Single();
            // loop through fitbook.userchat list
                // get the userchat list for each Chat
                // loop though the returned userchat list
                    // add each id into the list
            for(int i = 0; i < fitbookUser.UserChat.Count; i++)
            {
                var chat = _context.Chats.Include("UserChat").Where(c => c.ChatId == fitbookUser.UserChat[i].ChatId).Single();
                
                for(int j = 0; j < chat.UserChat.Count; j++)
                {
                    fitbookUserIds.Add(chat.UserChat[j].FitbookUserId);
                }
            }

            fitbookUserIds = fitbookUserIds.Distinct().ToList();
            fitbookUsers = _context.FitbookUsers.Select(f => f).ToList();
            // Removes users that have IDs in fitbookUserIds list
            fitbookUsers = RemoveConnectedUsers(fitbookUsers, fitbookUserIds);

            return View(fitbookUsers);
        }

        private List<FitbookUser> RemoveConnectedUsers(List<FitbookUser> fitbookUsers, List<int> fitbookuserIds)
        {
            for(int i = 0; i < fitbookuserIds.Count; i++)
            {
                for (int j = 0; j < fitbookUsers.Count; j++)
                {
                    if(fitbookUsers[j].FitbookUserId == fitbookuserIds[i])
                    {
                        fitbookUsers.RemoveAt(j);
                    }
                }
            }

            return fitbookUsers;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat(int fitbookUserId)
        {
            string currentAppUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            FitbookUser targetedUser = await _context.FitbookUsers.FindAsync(fitbookUserId);
            FitbookUser currentUser = _context.FitbookUsers.FirstOrDefault(f => f.ApplicationUserId.Equals(currentAppUserId));
            Chat chat = new Chat();
            targetedUser.Chats.Add(chat);
            currentUser.Chats.Add(chat);
            chat.FitbookUsers.Add(targetedUser);
            chat.FitbookUsers.Add(currentUser);
            UserChat userChat1 = new UserChat()
            {
                FitbookUser = currentUser,
                Chat = chat
            };
            UserChat userChat2 = new UserChat()
            {
                FitbookUser = targetedUser,
                Chat = chat
            };

            chat.UserChat.Add(userChat1);
            chat.UserChat.Add(userChat2);

            await _context.Chats.AddAsync(chat);
            await _context.SaveChangesAsync();

            Chat chatroom = _context.Chats.Include("UserChat").Where(c => c == chat).Single();
            
            return RedirectToAction("Chatroom", "Chat", new { chatroom.ChatId });
        }

        public IActionResult Chatroom(int chatroomId)
        {
            string currentAppUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            FitbookUser fitbookUser = _context.FitbookUsers.Where(f => f.ApplicationUserId.Equals(currentAppUserId)).Single();
            ViewBag.user = fitbookUser.FirstName + fitbookUser.LastName;

            return View();
        }
    }
}
