using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TasksWebService.Models;

namespace TaskAPI.Models
{
    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<TaskContext>());
            }
        }

        public static void SeedData(TaskContext context)
        {
            System.Console.WriteLine("Applying Migrations...");

            context.Database.Migrate();
            if (!context.TaskItems.Any())
            {
                Console.WriteLine("Adding data = seeding...");
                context.TaskItems.AddRange(
                new Tasks
                {
                    Title = "Schedule Daily Sales Report",
                    Description = "Report to list sales information at the end of the day",
                    Status = 400,
                    StartDate = new DateTime(2020, 5, 1),
                    EndDate = new DateTime(2020, 7, 20),
                    Priority = 5,
                    Category = "Green"
                },
                new Tasks
                {
                    Title = "Inventory Listing Report",
                    Description = "Run a report to see the state of client's inventory",
                    Status = 200,
                    StartDate = new DateTime(2019, 1, 1),
                    EndDate = new DateTime(2020, 1, 1),
                    Priority = 1,
                    Category = "Blue"
                }
                );

                context.SaveChanges();
            } 
            else
            {
                Console.WriteLine("Already have data");
            }

        }
    }
}
