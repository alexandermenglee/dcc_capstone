using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fitbook.Interfaces;
using System.Security.Claims;
using Fitbook.Models;

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
        public ActionResult Index()
        {

            string appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DateTime today = DateTime.Today;

            // check if current user has a day already
            if(_dayRepository.CheckDateExists(appUserId))
            {

                Day currentDailyLog = _dayRepository.DisplayDailyLog(appUserId, today);

                return View(currentDailyLog);
            }

            _dayRepository.Create(appUserId);
            _dayRepository.DisplayDailyLog(appUserId, today);

            return View();
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
    }
}