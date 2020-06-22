using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BettingRoulette.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BettingRoulette
{
    public class Startup
    {
        private static string DataBaseHost = Environment.GetEnvironmentVariable("DataBaseHost");
        private static string DataBaseUser = Environment.GetEnvironmentVariable("DataBaseUser");
        private static string DataBasePassword = Environment.GetEnvironmentVariable("DataBasePassword");
        private static string DataBaseName = Environment.GetEnvironmentVariable("DataBaseName");
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RouletteContext>(opt =>
            opt.UseNpgsql($"Host = {DataBaseHost}; Database = {DataBaseName}; Username = {DataBaseUser}; Password = {DataBasePassword}"));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
