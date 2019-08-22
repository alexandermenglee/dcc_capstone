using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fitbook.Controllers
{
    public class MockServiceRegister : IMockServiceRegister
    {
        public async Task<ActionResult> Add(FitbookUser fbUser)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
