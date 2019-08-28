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
        public void DisplayDailyLog(string appUserId)
        {
            DateTime currentDay = DateTime.Today;
            Day today = new Day();
            try
            {
                FitbookUser fitbookUser = _context.FitbookUsers.Where(f => f.ApplicationUserId.Equals(appUserId)).Single();

                // compare 
            }
            catch(Exception E)
            {
                // should redirect to a view with an error message
            }
        }
    }
}
