using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Data;
using Fitbook.Interfaces;
using Fitbook.Models;
using Microsoft.AspNetCore.Mvc;

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
                today.Nutrition = fitbookUsersMacronutrients;

                await _context.AddAsync(today);
                await _context.SaveChangesAsync();
        }

        public bool CheckDateExists(string appUserId, DateTime day)
        {
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

        public Day DisplayDailyLog(string appUserId, DateTime day)
        {
            // search database for Day with current day and fitbookuserId
            throw new NotImplementedException();
        }
    }
}
