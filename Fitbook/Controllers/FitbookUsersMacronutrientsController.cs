using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fitbook.Models;
using Fitbook.Classes;
using Fitbook.Interfaces;
using System.Security.Claims;

namespace Fitbook.Controllers
{
    public class FitbookUsersMacronutrientsController : Controller
    {
        IFitbookUsersMacronutrientsRepsitory _fitbookUsersMacronutrientsRepsitory;

        public FitbookUsersMacronutrientsController(IFitbookUsersMacronutrientsRepsitory fitbookUsersMacronutrientsRepsitory)
        {
            _fitbookUsersMacronutrientsRepsitory = fitbookUsersMacronutrientsRepsitory;
        }

        // GET: FitbookUsersMacronutrients
        public ActionResult Index()
        {
            return View();
        }

        // GET: FitbookUsersMacronutrients/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FitbookUsersMacronutrients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FitbookUsersMacronutrients/Create
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

        // GET: FitbookUsersMacronutrients/Edit/5
        public ActionResult Edit(int id)
        {

            return View();
        }

        // POST: FitbookUsersMacronutrients/Edit/5
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
                return RedirectToAction("");
            }
        }

        public ActionResult SelectMacronutrientSplit()
        {
            return View();
        }

        [HttpPost]
        public void SubmitMacronutrientSplit([FromForm]IFormCollection form)
        {
            var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var macros = _fitbookUsersMacronutrientsRepsitory.FindByApplicationUserId(appUserId);
        }

        [HttpPost]
        public void SubmitCustomMacronutrientSplit([FromForm]int carbs, int protein, int fat)
        {
            var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var macros = _fitbookUsersMacronutrientsRepsitory.FindByApplicationUserId(appUserId);
        }

        public ActionResult SelectDietPlan()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitDietPlan([FromForm]IFormCollection form)
        {
            var fitnessGoalValue = form["fitness-goal"];
            var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                _fitbookUsersMacronutrientsRepsitory.CalculateMacros(Convert.ToInt32(fitnessGoalValue), appUserId);
            }
            catch(Exception e)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

    }
}