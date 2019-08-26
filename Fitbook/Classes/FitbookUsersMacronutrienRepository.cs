using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Interfaces;
using Fitbook.Models;
using Fitbook.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fitbook.Classes
{
    public class FitbookUsersMacronutrienRepository : IFitbookUsersMacronutrientsRepsitory
    {
        private readonly ApplicationDbContext _context;

        public FitbookUsersMacronutrienRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public FitbookUsersMacronutrients FindByApplicationUserId(string appUserId)
        {
            FitbookUsersMacronutrients macros;
            var fitbookUser = _context.FitbookUsers.Where(f => f.ApplicationUserId.Equals(appUserId)).Single();
            macros = _context.FitbookUsersMacronutrients.Where(m => m.FitbookUserId == fitbookUser.FitbookUserId).Single();

            return macros;
        }
        public async Task<bool> Add(int fitbookUserId)
        {
            var macroObjects = _context.FitbookUsersMacronutrients.Where(f => f.FitbookUserId == fitbookUserId).ToList();

            if (macroObjects.Count != 0)
            {
                return false;
            }

            var fitBookUser = _context.FitbookUsers.Where(f => f.FitbookUserId == fitbookUserId).Single();

            FitbookUsersMacronutrients macros = new FitbookUsersMacronutrients()
            {
                FitbookUserId = fitbookUserId,
                DailyCalories = DailyCalories.CalculateDailyCalories(fitBookUser)
            };

            await _context.FitbookUsersMacronutrients.AddAsync(macros);
            await _context.SaveChangesAsync();

            return true;
        }

        public void AddMacronutrients(int macroId, string recommendedSplit)
        {
            FitbookUsersMacronutrients macros;
            switch(recommendedSplit)
            {
                case "high carbs":
                    macros = _context.FitbookUsersMacronutrients.Where(m => m.FitbookUserId == macroId).Single();
                    macros.Carbohydrates = Convert.ToInt32((.4 * macros.DailyCalories) / 4);
                    macros.Protein = Convert.ToInt32((.3 * macros.DailyCalories) / 4);
                    macros.Fat = Convert.ToInt32((.3 * macros.DailyCalories) / 9);
                    _context.SaveChanges();
                    break;
                case "high protein":
                    macros = _context.FitbookUsersMacronutrients.Where(m => m.FitbookUserId == macroId).Single();
                    macros.Carbohydrates = Convert.ToInt32((.4 * macros.DailyCalories) / 4);
                    macros.Protein = Convert.ToInt32((.4 * macros.DailyCalories) / 4);
                    macros.Fat = Convert.ToInt32((.2 * macros.DailyCalories) / 9);
                    _context.SaveChanges();
                    break;
                case "balanced":
                    macros = _context.FitbookUsersMacronutrients.Where(m => m.FitbookUserId == macroId).Single();
                    macros.Carbohydrates = Convert.ToInt32((.35 * macros.DailyCalories) / 4);
                    macros.Protein = Convert.ToInt32((.35 * macros.DailyCalories) / 4);
                    macros.Fat = Convert.ToInt32((.3 * macros.DailyCalories) / 9);
                    _context.SaveChanges();
                    break;
                default:
                    break;
            }
        }

        public void AddMacronutrients(int macroId, int carbs, int protein, int fat)
        {
            FitbookUsersMacronutrients macros = _context.FitbookUsersMacronutrients.Find(macroId);
            double carbPercentage = (double)carbs / (double)100;
            double proteinPercentage = (double)protein / (double)100;
            double fatPercentage = (double)fat / (double)100;

            macros.Carbohydrates = Convert.ToInt32((carbPercentage * macros.DailyCalories) / 4);
            macros.Protein = Convert.ToInt32((proteinPercentage * macros.DailyCalories) / 4);
            macros.Fat = Convert.ToInt32((fatPercentage * macros.DailyCalories) / 9);

            _context.SaveChanges();
        }

        public void CalculateMacros(int fitnessGoalValue, string appUserId)
        {

            try
            {
                var fitbookUser = _context.FitbookUsers.Where(f => f.ApplicationUserId.Equals(appUserId)).Single();
                var macros = _context.FitbookUsersMacronutrients.Where(m => m.FitbookUserId == fitbookUser.FitbookUserId).Single();

                int calories = DailyCalories.CalculateFitnessGoals(macros.DailyCalories, fitnessGoalValue);
            }
            catch
            {

            }
        }

        public void CalculateFitnessGoal(int value)
        {
            
        }
    }
}
