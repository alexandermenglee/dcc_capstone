using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Data;
using Fitbook.Interfaces;
using Fitbook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fitbook.ViewModel;

namespace Fitbook.Classes
{
    public class DayRespository : IDayRepository
    {
        ApplicationDbContext _context;
        public DayRespository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Create(string appUserId)
        {
            DateTime currentDay = DateTime.Today;

                Day today = new Day();
                int fitbookUserId = _context.FitbookUsers.Where(f => f.ApplicationUserId.Equals(appUserId)).Single().FitbookUserId;
                var fitbookUsersMacronutrients = _context.FitbookUsersMacronutrients.Where(m => m.FitbookUserId == fitbookUserId).Single();

                today.FitbookUserId = fitbookUserId;
                today.Date = currentDay;

                await _context.AddAsync(today);
                await _context.SaveChangesAsync();
        }

        public bool CheckDateExists(string appUserId)
        {
            DateTime day = DateTime.Today;

            try
            {
                int fitbookUserId = _context.FitbookUsers.Where(f => f.ApplicationUserId.Equals(appUserId)).Single().FitbookUserId;
                var dateExists = _context.Days.Where(d => d.Date == day && d.FitbookUserId == fitbookUserId).ToList();

                if (dateExists.Count > 0)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public List<List<Meal>> GetMeals(string appUserId, DateTime date)
        {
            // Query for current user 
            // Query for specified day associated to found user
            // Query for meals where meals.DayId equals specified day
            // For each meal, run a query to retrieve all foods in each meal (MealFood)

            List<List<Meal>> allMealsForDay = new List<List<Meal>>();

            // Gets current user
            int fitbookUserId = _context.FitbookUsers.Where(f => f.ApplicationUserId.Equals(appUserId)).Single().FitbookUserId;
            // Gets current day by associtaed user and associted day
            // Includes all meals for that day
            Day desiredDisplayDay = _context.Days.Include("Meals").Where(d => d.Date == date && d.FitbookUserId == fitbookUserId).Single();
            
            // Takes the MealId of each meal in desiredDisplayDay.Meals and queries for associated MealFoods list
            // Adds the list of type Meal to allMealsForDay list
            for(int i = 0; i < desiredDisplayDay.Meals.Count; i++)
            {
                IQueryable<Meal> iqueryableMeals = _context.Meals.Include("MealFoods").Where(m => m.MealId == desiredDisplayDay.Meals[i].MealId);

                allMealsForDay.Add(iqueryableMeals.ToList());
            }

            return allMealsForDay;
        }
    }
}
