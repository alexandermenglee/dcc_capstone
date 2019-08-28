using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fitbook.Interfaces
{
    public interface IDayRepository
    {
        IActionResult Index(Day id);
    }
}
