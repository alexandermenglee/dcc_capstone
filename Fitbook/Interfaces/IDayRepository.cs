using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Models;

namespace Fitbook.Interfaces
{
    public interface IDayRepository
    {
        void DisplayDailyLog(string appUserId);
    }
}
