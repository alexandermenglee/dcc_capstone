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

        public async Task SubmitQuery(string query)
        {
           List<Food> searchResults = await SearchNutritionXiAPI(query);
        }

        /*[HttpPost]
        public async Task SearchFood(int mealId)
        {
            await SearchNutritionXiAPI();
        }*/

        // Method to make API call to NutritionXi API
        private async Task<List<Food>> SearchNutritionXiAPI(string food)
        {
            _client.DefaultRequestHeaders.Add("x-app-id", API_Keys.NutrionixAPIKey.APPLICATION_ID);
            _client.DefaultRequestHeaders.Add("x-app-key", API_Keys.NutrionixAPIKey.API_KEY);

            var results = await _client.GetStringAsync($"https://trackapi.nutritionix.com/v2/search/instant?query={food}");
            var deserializedResults = JsonConvert.DeserializeObject<JObject>(results);
            var commonFoodList = deserializedResults["common"].ToList();
            var brandedFoodList = deserializedResults["branded"].ToList();
            List<Food> searchResults = new List<Food>();

            for (int i = 0; i < commonFoodList.Count; i++)
            {
                Food newFood = new Food();
                string foodName = (string)commonFoodList[i]["food_name"];
                newFood.FoodName = foodName;
                searchResults.Add(newFood);
            }

            for(int i = 0; i < brandedFoodList.Count; i++)
            {
                Food newFood = new Food();
                string foodName = (string)brandedFoodList[i]["food_name"];
                newFood.FoodName = foodName;
                searchResults.Add(newFood);
            }

            return searchResults;
        }
    }
}