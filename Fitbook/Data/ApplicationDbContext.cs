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
            modelBuilder.Entity<Day>().HasMany(d => d.Meals).WithOne(m => m.Day);
        }
    }
}
