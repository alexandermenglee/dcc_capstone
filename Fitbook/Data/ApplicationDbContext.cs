using System;
using System.Collections.Generic;
using System.Text;
using Fitbook.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fitbook.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<CustomRecipe> CustomRecipes { get; set; }
        public DbSet<FitbookUser> FitbookUsers { get; set; }
        public DbSet<FitbookUsersMacronutrients> FitbookUsersMacronutrients { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<RecommendedRecipe> ReccommendedRecipes { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Creating one to many relationship between Day and Meal
            modelBuilder.Entity<Day>().HasMany(d => d.Meals).WithOne(m => m.Day);   

            // Creating junction table for Meal and Food (many to many relationship)
            modelBuilder.Entity<MealFood>().HasKey(mf => new { mf.MealId, mf.FoodId });
            modelBuilder.Entity<MealFood>().HasOne(mf => mf.Meal).WithMany(mf => mf.MealFoods).HasForeignKey(mf => mf.MealId);
            modelBuilder.Entity<MealFood>().HasOne(mf => mf.Food).WithMany(mf => mf.MealFoods).HasForeignKey(mf => mf.FoodId);

            // Creating junction table for FitbookUser and Chat (many to many relationship)
            modelBuilder.Entity<UserChat>().HasKey(uc => new {  uc.FitbookUserId, uc.ChatId });
            modelBuilder.Entity<UserChat>().HasOne(uc => uc.FitbookUser).WithMany(uc => uc.UserChat).HasForeignKey(uc => uc.FitbookUserId);
            modelBuilder.Entity<UserChat>().HasOne(uc => uc.Chat).WithMany(uc => uc.UserChat).HasForeignKey(uc => uc.ChatId);


            // Seeding Food table
            List<Food> FoodSeedingList = new List<Food>();

            Food food1 = new Food()
            {
                FoodId = -1,
                FoodName = "chicken breast",
                Carbohydrates = 0,
                Protein = 25,
                Fat = 1,
                Amount = 4
            };
            Food food2 = new Food()
            {
                FoodId = -2,
                FoodName = "brown rice",
                Carbohydrates = 37,
                Protein = 7,
                Fat = 0,
                Amount = 1
            };
            Food food3 = new Food()
            {
                FoodId = -3,
                FoodName = "olive oil",
                Carbohydrates = 0,
                Protein = 0,
                Fat = 16,
                Amount = 2
            };

            FoodSeedingList.Add(food1);
            FoodSeedingList.Add(food2);
            FoodSeedingList.Add(food3);

            modelBuilder.Entity<Food>().HasData(FoodSeedingList);

            // Seeding Meal Table
            List<Meal> MealSeedingList = new List<Meal>();

            Meal meal1 = new Meal()
            {
                MealId = -1
            };
            Meal meal2 = new Meal()
            {
                MealId = -2
            };

            MealSeedingList.Add(meal1);
            MealSeedingList.Add(meal2);

            modelBuilder.Entity<Meal>().HasData(MealSeedingList);

            // Seeding Category Table
            /*List<Category> categories = new List<Category>();

            Category category1 = new Category()
            {
                CategoryId = 1,
                CategoryName = "High Carb"
            };

            Category category2 = new Category()
            {
                CategoryId = 1,
                CategoryName = "High Protein"
            };

            Category category3 = new Category()
            {
                CategoryId = 3,
                CategoryName = "High Fat"
            };

            categories.Add(category1);
            categories.Add(category2);
            categories.Add(category3);

            modelBuilder.Entity<Category>().HasData(categories);*/
        }
    }
}
