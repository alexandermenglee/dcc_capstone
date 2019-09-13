using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Models;

namespace Fitbook.Interfaces
{
    public interface IDayRepository
    {
        Task Create(string appUserId);
        bool CheckDateExists(string appUserId);
        Task<List<Meal>> GetMeals(string appUserId, DateTime date);
        Day GetDay(string appUserId, DateTime date);
        void AddMealToDay(int dayId);
        Dictionary<string, int> GetNutrition(List<Meal> meals);
        Task<FitbookUser> GetFBUser(string appUserId);
        Task<FitbookUsersMacronutrients> GetMacros(FitbookUser fitbookUser);
        Task SaveDayNutrition(Day day, Dictionary<string, int> nutrition);
    }

}
