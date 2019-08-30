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
        }
    }
}
