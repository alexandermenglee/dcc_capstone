using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fitbook.Data;
using Fitbook.Models;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fitbook.Controllers
{
    public class CustomRecipeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomRecipeController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            List<CustomRecipe> allRecipesFromDb = _context.CustomRecipes.Select(c => c).ToList();

            return View(allRecipesFromDb);
        }

        public IActionResult AddRecipe()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipe(CustomRecipe customRecipe)
        {
            string appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            FitbookUser fitbookUser = _context.FitbookUsers.Where(f => f.ApplicationUserId.Equals(appUserId)).Single();

            customRecipe.FitbookUserId = fitbookUser.FitbookUserId;

            await _context.CustomRecipes.AddAsync(customRecipe);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
