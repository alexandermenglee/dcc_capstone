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
using System.Net.Http;

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
            Meal initialMeal = new Meal();
            today.Meals.Add(initialMeal);
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

        public async Task<List<Meal>> GetMeals(string appUserId, DateTime date)
        {
            // Query for current user 
            // Query for specified day associated to found user
            // Query for meals where meals.DayId equals specified day
            // For each meal, run a query to retrieve all foods in each meal (MealFood)

            List<Meal> allMealsForDay = new List<Meal>();

            // Gets current user
            FitbookUser fitbookUserId = await _context.FitbookUsers.FirstOrDefaultAsync(f => f.ApplicationUserId.Equals(appUserId));

            // Gets current day by associtaed user and associated day
            // Includes all meals for that day
            Day desiredDisplayDay = _context.Days.Include("Meals").Where(d => d.Date == date && d.FitbookUserId == fitbookUserId.FitbookUserId).Single();
            
            // Takes the MealId of each meal in desiredDisplayDay.Meals and queries for associated MealFoods list
            // Adds the list of type Meal to allMealsForDay list
            for(int i = 0; i < desiredDisplayDay.Meals.Count; i++)
            {
                Meal mealWithMealFoodsList = _context.Meals.Include("MealFoods").Where(m => m.MealId == desiredDisplayDay.Meals[i].MealId).Single();

                allMealsForDay.Add(mealWithMealFoodsList);
            }

            // Loop through all MealLists and get food by id 
            for(int i = 0; i < allMealsForDay.Count; i++)
            {
                for(int j = 0; j < allMealsForDay[i].MealFoods.Count; j++)
                {
                    Food food = _context.Foods.Where(f => f.FoodId == allMealsForDay[i].MealFoods[j].FoodId).Single();
                    allMealsForDay[i].Foods.Add(food);
                }
            }

            return allMealsForDay;
        }

        public Dictionary<string, int> GetNutrition(List<Meal> meals)
        {
            Dictionary<string, int> nutrition = new Dictionary<string, int>();
            nutrition.Add("carbohydrates", 0);
            nutrition.Add("protein", 0);
            nutrition.Add("fat", 0);
            nutrition.Add("calories", 0);

            for (int i = 0; i < meals.Count; i++)
            {
                List<Food> currentFoodList = meals[i].Foods;
                for (int j = 0; j < currentFoodList.Count; j++)
                {
                    // loop through each food to calculate nutrition
                    nutrition["carbohydrates"] += currentFoodList[j].Carbohydrates;
                    nutrition["protein"] += currentFoodList[j].Protein;
                    nutrition["fat"] += currentFoodList[j].Fat;
                    nutrition["calories"] += currentFoodList[j].Calories;
                }
            }

            return nutrition;
        }

        public Day GetDay(string appUserId, DateTime date)
        {
            FitbookUser fitbookUser = _context.FitbookUsers.Where(f => f.ApplicationUserId.Equals(appUserId)).Single();

            return _context.Days.Where(d => d.FitbookUserId == fitbookUser.FitbookUserId && d.Date == date).Single();
        }

        public void AddMealToDay(int dayId)
        {
            Day day = _context.Days.Find(dayId);
            Meal meal = new Meal();
            day.Meals.Add(meal);

            _context.SaveChanges();
        }
    }
}
