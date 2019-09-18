using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fitbook.Data;
using Fitbook.Models;
using System.Security.Claims;
using Fitbook.ViewModel;

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
            CustomRecipeIndexViewModel viewModel = new CustomRecipeIndexViewModel();
            
            viewModel.customRecipes = _context.CustomRecipes.Select(c => c).ToList();
            viewModel.fitbookUsers = GetAssociatedUsers(viewModel.customRecipes);
            viewModel.Categories = _context.Categories.Select(c => c).ToList();
            
            return View(viewModel);
        }

        // Helper method to get associated fitbookusers and their submitted recipes
        private List<FitbookUser> GetAssociatedUsers(List<CustomRecipe> customRecipes)
        {
            List<FitbookUser> associatedFitbookUsers = new List<FitbookUser>();
            
            foreach(CustomRecipe customRecipe in customRecipes)
            {
                FitbookUser fitbookUser = _context.FitbookUsers.Where(f => f.FitbookUserId == customRecipe.FitbookUserId).Single();
                associatedFitbookUsers.Add(fitbookUser);
            }

            return associatedFitbookUsers;
        }

        public IActionResult AddRecipe()
        {
            AddRecipeViewModel viewModel = new AddRecipeViewModel();
            viewModel.Categories = _context.Categories.Select(c => c).ToList();

            return View(viewModel);
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

        [HttpGet]
        public async Task<IActionResult> Display(int id)
        {
            CustomRecipe customRecipe = await _context.CustomRecipes.FindAsync(id);

            return View(customRecipe);
        }

        [HttpGet]
        public IActionResult FilterByCategory(int id)
        {
            CustomRecipeIndexViewModel viewModel = new CustomRecipeIndexViewModel();
            viewModel.customRecipes = _context.CustomRecipes.Where(c => c.CategoryId == id).ToList();
            viewModel.fitbookUsers = GetAssociatedUsers(viewModel.customRecipes);

            return View(viewModel);
        }
    }
}
