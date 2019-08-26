using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fitbook.Interfaces
{
    public interface IFitbookUsersMacronutrientsRepsitory
    {
        FitbookUsersMacronutrients FindByApplicationUserId(string appUserId);
        Task<bool> Add(int fitbookUserId);
        void CalculateCaloricGoal(int fitnessGoalValue, string appUserId);
        void AddPredefinedMacronutrients(int macroId, string recommendedSplit);
        void AddCustomMacronutrients(int macroId, int carbs, int protein, int fat);
    }
}
