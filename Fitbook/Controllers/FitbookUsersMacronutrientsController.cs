using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fitbook.Models;
using Fitbook.Classes;

namespace Fitbook.Controllers
{
    public class FitbookUsersMacronutrientsController : Controller
    {
        ApplicationDbContext _context;

        public FitbookUsersMacronutrientsController(ApplicationDbContext context)
        {
            _context = context;
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

        public async Task<IActionResult> Add(int fitbookUserId)
        {
            var macroObjects = _context.FitbookUsersMacronutrients.Where(f => f.FitbookUserId == fitbookUserId).ToList();

            if(macroObjects.Count != 0)
            {
                return RedirectToAction("Index", "FitbookUser");
            }

            var fitBookUser = _context.FitbookUsers.Where(f => f.FitbookUserId == fitbookUserId).Single();

            FitbookUsersMacronutrients macros = new FitbookUsersMacronutrients()
            {
                FitbookUserId = fitbookUserId,
                DailyCalories = DailyCalories.CalculateDailyCalories(fitBookUser)
            };

            await _context.FitbookUsersMacronutrients.AddAsync(macros);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "FitbookUser");
        }
    }
}