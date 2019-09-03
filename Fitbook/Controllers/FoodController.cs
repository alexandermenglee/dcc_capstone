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

        // GET: Food/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Food/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Food/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult SearchFood()
        {
            return View();
        }

        public async Task<IActionResult> SubmitQuery(string query)
        {
            FoodQueryResultsViewModel foodQueryResults = new FoodQueryResultsViewModel();
            List<Food> searchResults = await SearchNutritionXiAPI(query);

            foodQueryResults.queryResults = searchResults;

            return View(foodQueryResults);
        }

        // Method to make API call to NutritionXi API
        private async Task<List<Food>> SearchNutritionXiAPI(String query)
        {
            _client.DefaultRequestHeaders.Add("x-app-id", API_Keys.NutrionixAPIKey.APPLICATION_ID);
            _client.DefaultRequestHeaders.Add("x-app-key", API_Keys.NutrionixAPIKey.API_KEY);

            var results = await _client.GetStringAsync($"https://trackapi.nutritionix.com/v2/search/instant?query={query}");
            var deserializedResults = JsonConvert.DeserializeObject<JObject>(results);
            var brandedFoodList = deserializedResults["branded"].ToList();
            List<Food> searchResults = new List<Food>();

            for(int i = 0; i < brandedFoodList.Count; i++)
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
    }
}