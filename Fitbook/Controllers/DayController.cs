using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fitbook.Interfaces;

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

        // GET: Day/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Day/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}