using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitbook.Models;

namespace Fitbook.Classes
{
    public static class DailyCalories
    {
        private static string male = "Male";

        public static int CalculateDailyCalories(FitbookUser fitBookUser)
        {
            switch (fitBookUser.Lifestyle)
            {
                case "Sedentary":
                    return Convert.ToInt32(CalculateBMR(fitBookUser) * 1.2);
                case "Lightly Active":
                    return Convert.ToInt32(CalculateBMR(fitBookUser) * 1.375);
                case "Moderately Active":
                    return Convert.ToInt32(CalculateBMR(fitBookUser) * 1.375);
                case "Very Active":
                    return Convert.ToInt32(CalculateBMR(fitBookUser) * 1.55);
                case "Extremely Active":
                    return Convert.ToInt32(CalculateBMR(fitBookUser) * 1.9);
                default:
                    return 0;
            }
        }

        public static int CalculateBMR(FitbookUser fitbookUser)
        {
            if (fitbookUser.Gender.Equals(male))
            {
                return CalculateWithHarrisBenedictEquationMale(fitbookUser.Height, fitbookUser.Weight, fitbookUser.Age);
            }
            else
            {
                return CalculateWithHarrisBenedictEquationFemale(fitbookUser.Height, fitbookUser.Weight, fitbookUser.Age);
            }
        }

        // Male
        private static int CalculateWithHarrisBenedictEquationMale(int? height, int? weight, int? age)
        {
            return Convert.ToInt32(66 + (6.2 * weight) + (12.7 * height) - (6.76 * age));
        }

        // Female
        private static int CalculateWithHarrisBenedictEquationFemale(int? height, int? weight, int? age)
        {
            return Convert.ToInt32(655.1 + (4.35 * weight) + (4.7 * height) - (4.7 * age));
        }
    }
}
