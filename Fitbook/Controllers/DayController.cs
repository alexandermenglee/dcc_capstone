using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fitbook.Interfaces;
using System.Security.Claims;
using Fitbook.Models;
using Fitbook.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace Fitbook.Controllers
{
    [Authorize]
    public class DayController : Controller
    {
        IDayRepository _dayRepository;

        public DayController(IDayRepository dayRepository)
        {
            _dayRepository = dayRepository;
        }
        // GET: Day
        public async Task<ActionResult> Index()
         {
            FitbookUser fitbookUser;
            FitbookUsersMacronutrients fbUsersNutrients;
            IndexViewModel indexViewModel = new IndexViewModel();
            List<Meal> meals;
            Dictionary<string, int> nutrition; 

            string appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DateTime today = DateTime.Today;

            // check if current user has a day already

            if (!_dayRepository.CheckDateExists(appUserId))
            {
                await _dayRepository.Create(appUserId);
            }

            meals = await _dayRepository.GetMeals(appUserId, today);
            fitbookUser = await _dayRepository.GetFBUser(appUserId);
            fbUsersNutrients = await _dayRepository.GetMacros(fitbookUser);

            indexViewModel.Meals = meals;
            indexViewModel.fitbookUser = fitbookUser;
            indexViewModel.fitbookUsersMacronutrients = fbUsersNutrients;
            indexViewModel.Day = _dayRepository.GetDay(appUserId, today);

            nutrition = _dayRepository.GetNutrition(meals);
            indexViewModel.Day.Carbohydates = nutrition["carbohydrates"];
            indexViewModel.Day.Fat = nutrition["protein"];
            indexViewModel.Day.Protein = nutrition["fat"];
            indexViewModel.Day.Calories = nutrition["calories"];

            await _dayRepository.SaveDayNutrition(indexViewModel.Day, nutrition);

            return View(indexViewModel);
        }

        public IActionResult AddMeal(int dayId)
        {
            _dayRepository.AddMealToDay(dayId);

            return RedirectToAction("Index");
        }
    }
}