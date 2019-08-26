using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Fitbook.Data;
using Fitbook.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fitbook.Classes;
using Fitbook.Interfaces;

namespace Fitbook.Controllers
{
    public class FitbookUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFitbookUsersMacronutrientsRepsitory _fitbookUsersMacronutrientsRespository;
        public FitbookUserController(ApplicationDbContext context, IFitbookUsersMacronutrientsRepsitory fitbookUsersMacronutrientsRespository)
        {
            _context = context;
            _fitbookUsersMacronutrientsRespository = fitbookUsersMacronutrientsRespository;
        }

        // GET: FitbookUser
        public ActionResult Index()
        {
            var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(appUserId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var fitbookUser = _context.FitbookUsers.FirstOrDefault(f => f.ApplicationUserId.Equals(appUserId));

            return View(fitbookUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FitbookUser/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FitbookUser fitBookUserChanges)
        {
            try
            {
                // TODO: Add update logic here
                var fitBookUser = _context.FitbookUsers.Attach(fitBookUserChanges);
                fitBookUser.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // Create Fitbook User 
        public ActionResult EnterStatistics()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SubmitStatistics(FitbookUser fitbookUser)
        {
            var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            fitbookUser.ApplicationUserId = appUserId;

            await _context.FitbookUsers.AddAsync(fitbookUser);
            await _context.SaveChangesAsync();

            return RedirectToAction("ChooseLifestyle");
        }

        public ActionResult ChooseLifestyle()
        {
            return View();
        }

        public async Task<IActionResult> SubmitLifestyle(string lifestyle)
        {
            var currentSignedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var fitbookUser = _context.FitbookUsers.Where(f => f.ApplicationUserId.Equals(currentSignedInUserId)).Single();

            fitbookUser.Lifestyle = lifestyle;
            await _context.SaveChangesAsync();

            if(!await _fitbookUsersMacronutrientsRespository.Add(fitbookUser.FitbookUserId))
            {
                return RedirectToAction("Index", "Index");
            }

            return RedirectToAction("SelectDietPlan", "FitbookUsersMascronutrients");
        }
    }
}