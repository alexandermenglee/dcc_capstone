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

        public ActionResult SelectDietPlan()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitDietPlan([FromForm]IFormCollection form)
        {
            var fitnessGoalValue = form["fitness-goal"];

            try
            {
                int x = Convert.ToInt32(fitnessGoalValue);
            }
            catch(Exception e)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

    }
}