using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Fitbook.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fitbook.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Fitbook.API_Keys;
using Newtonsoft.Json.Linq;
using Fitbook.ViewModel;

namespace Fitbook.Controllers
{
    public class FoodController : Controller
    {
        ApplicationDbContext _context;
        private static HttpClient _client;

        public FoodController(ApplicationDbContext context)
        {
            _context = context;
            _client = new HttpClient();
        }

        // GET: Food
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchFood(int mealId)
        {
            return View(mealId);
        }

        // Creates a ViewModel that contains a List<Food> member property
        // Calls the SearchNutritionXiAPI method to get a list of Food objects
        // Sets the ViewModels List<Food> to the list returned from the API call
        // Returns the view with the ViewModel passed in
        public async Task<IActionResult> SubmitQuery(string query, int mealId)
        {
            FoodQueryResultsViewModel foodQueryResults = new FoodQueryResultsViewModel();
            List<Food> searchResults = await SearchNutritionXiAPI(query);

            foodQueryResults.queryResults = searchResults;
            foodQueryResults.MealId = mealId;

            return View(foodQueryResults);
        }


        [HttpPost]
        public async Task<IActionResult> AddFoodToMeal(IFormCollection form)
        {
            string foodName = form["foodName"];
            int proteinCount = Int32.Parse(form["protein"]);

            Food food = new Food()
            {
                FoodName = form["foodName"],
                Carbohydrates = Int32.Parse(form["carbohydrates"]),
                Protein = Int32.Parse(form["protein"]),
                Fat = Int32.Parse(form["fat"]),
                Calories = Int32.Parse(form["calories"]),
                NIX_ID = form["nixId"]
            };

            // check if food exists, if not add it to the database
            CheckFoodInDB(food);
           
            var meal = await _context.Meals.FindAsync(Int32.Parse(form["mealId"]));

            Food newFood = _context.Foods.Where(f => f.NIX_ID.Equals(food.NIX_ID)).Single();
            MealFood mealFood = new MealFood()
            {
                Meal = meal,
                Food = newFood
            };

            meal.MealFoods.Add(mealFood);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Day");
        }

        private bool CheckFoodInDB(Food food)
        {
            List<Food> foodInDB =  _context.Foods.Where(f => f.NIX_ID.Equals(food.NIX_ID)).ToList();
            
            if(foodInDB.Count > 0)
            {
                return true;
            }

            _context.Foods.Add(food);
            _context.SaveChanges();

            return false;
        }

        // Method to make API call to NutritionXi API
        // Gets all foods from NutrionXi API
        // Creates a Food object from each result
        // Returns a List<Food> containing all the Food objects created from the API call
        private async Task<List<Food>> SearchNutritionXiAPI(String query)
        {
            _client.DefaultRequestHeaders.Add("x-app-id", API_Keys.NutrionixAPIKey.APPLICATION_ID);
            _client.DefaultRequestHeaders.Add("x-app-key", API_Keys.NutrionixAPIKey.API_KEY);

            var results = await _client.GetStringAsync($"https://trackapi.nutritionix.com/v2/search/instant?query={query}");
            var deserializedResults = JsonConvert.DeserializeObject<JObject>(results);
            var brandedFoodList = deserializedResults["branded"].ToList();
            List<Food> searchResults = new List<Food>();

            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // CHANGE THIS BACK TO brandedFoodList.Count AFTER TESTING
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            for (int i = 0; i < 2; i++)
            {
                Food food = new Food();

                food.FoodName = (string)brandedFoodList[i]["food_name"];
                food.NIX_ID = (string)brandedFoodList[i]["nix_item_id"];
                JObject deserializedFood = await GetNutrition(food.NIX_ID);
                SetFoodValues(deserializedFood, food);

                searchResults.Add(food);
            }

            return searchResults;
        }

        private Food SetFoodValues(JObject deserializedFood, Food food)
        {
            food.Carbohydrates = (int)deserializedFood["foods"][0]["nf_total_carbohydrate"];
            food.Protein = (int)deserializedFood["foods"][0]["nf_protein"];
            food.Fat = (int)deserializedFood["foods"][0]["nf_total_fat"];
            food.Calories = (int)deserializedFood["foods"][0]["nf_calories"];

            return food;
        }

        private async Task<JObject> GetNutrition(string nixId)
        {
            string endpoint = $"https://trackapi.nutritionix.com/v2/search/item?nix_item_id={nixId}";

            /*_client.DefaultRequestHeaders.Add("x-app-id", API_Keys.NutrionixAPIKey.APPLICATION_ID);
            _client.DefaultRequestHeaders.Add("x-app-key", API_Keys.NutrionixAPIKey.API_KEY);*/

            var results = await _client.GetStringAsync(endpoint);

            return JsonConvert.DeserializeObject<JObject>(results);
        }

        public IActionResult SearchFoodInDatabase()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitSearchFoodInDatabase(string query)
        {
            List<Food> queryResults = _context.Foods.Where(f => f.FoodName.Contains(query)).ToList();

            return View(queryResults);
        }

        [HttpPost]
        public IActionResult CompareAgainstMacros(string nixId)
        {
            Food food = _context.Foods.Where(f => f.NIX_ID.Equals(nixId)).Single();
            return View();
        }
    }
}