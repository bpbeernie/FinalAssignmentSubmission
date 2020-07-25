
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Threading;
using TaskAPI.Models;
using TasksWebService.Models;

namespace TaskAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            var server = Configuration["DBServer"];
            var port = Configuration["DBPort"];
            var user = Configuration["DBUser"];
            var password = Configuration["DBPassword"];
            var database = Configuration["Database"];
            var connectionString = $"server={server};Port={port};Database={database};Uid={user};Pwd={password};";

            WaitForDbInit(connectionString);

            services.AddDbContext<TaskContext>(options => 
            options.UseMySql(
                connectionString, builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                }));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();

            PrepDB.PrepPopulation(app);
        }

        private static void WaitForDbInit(string connectionString)
        {
            var connection = new MySqlConnection(connectionString);
            Console.WriteLine("Connecting with credentials: {0}", connectionString);

            var retries = 1;
            while (retries < 5)
            {
                try
                {
                    Console.WriteLine("Connecting to db. Trial: {0}", retries);
                    connection.Open();
                    connection.Close();
                    break;
                }
                catch (MySqlException)
                {
                    Thread.Sleep((int)Math.Pow(2, retries) * 5000);
                    retries++;
                }
            }
        }
    }
}
