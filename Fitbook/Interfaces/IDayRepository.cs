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
        Day DisplayDailyLog(string appUserId, DateTime date);
    }
}
