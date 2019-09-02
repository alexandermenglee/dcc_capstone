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
    }

}
