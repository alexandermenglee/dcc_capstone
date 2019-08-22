using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fitbook.Controllers
{
    public interface IMockServiceRegister
    {
        Task<ActionResult> Add(FitbookUser fbUser);
        Task<ActionResult> Delete(int id);
    }
}
