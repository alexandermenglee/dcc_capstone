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

namespace Fitbook.Controllers
{
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
            IndexViewModel indexViewModel = new IndexViewModel();
            List<Meal> meals;
            Dictionary<string, int> nutrition; 

            string appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DateTime today = DateTime.Today;

            // check if current user has a day already
            if(_dayRepository.CheckDateExists(appUserId))
            {

                meals = await _dayRepository.GetMeals(appUserId, today);
                indexViewModel.Meals = meals;
                // set indexViewModel Day property
                
                indexViewModel.Day = _dayRepository.GetDay(appUserId, today);
                // set total nutrition for that day
                nutrition = _dayRepository.GetNutrition(meals);
                indexViewModel.Day.Carbohydates = nutrition["carbohydrates"];
                indexViewModel.Day.Fat = nutrition["protein"];
                indexViewModel.Day.Protein = nutrition["fat"];
                indexViewModel.Day.Calories= nutrition["calories"];

                return View(indexViewModel);
            }

            await _dayRepository.Create(appUserId);

            indexViewModel.Day = _dayRepository.GetDay(appUserId, today);
            indexViewModel.Meals = await _dayRepository.GetMeals(appUserId, today);

            return View(indexViewModel);
        }

        // GET: Day/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Day/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Day/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Day/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Day/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddFood(int mealId)
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddMeal(int dayId)
        {
            // get day by id
            _dayRepository.AddMealToDay(dayId);

            return RedirectToAction("Index");
        }
    }
}