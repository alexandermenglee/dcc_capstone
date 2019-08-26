using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Interfaces;
using Fitbook.Models;
using Fitbook.Data;
using Microsoft.AspNetCore.Mvc;

namespace Fitbook.Classes
{
    public class FitbookUsersMacronutrienRepository : IFitbookUsersMacronutrientsRepsitory
    {
        private readonly ApplicationDbContext _context;

        public FitbookUsersMacronutrienRepository(ApplicationDbContext context)
        {
            _context = context;
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

        public Task CalculateMacros()
        {
            throw new NotImplementedException();
        }
    }
}
