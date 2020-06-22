using BettingRoulette.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System;
namespace BettingRoulette
{
    public class Startup
    {
        private static string DataBaseHost = Environment.GetEnvironmentVariable("DataBaseHost");
        private static string DataBaseUser = Environment.GetEnvironmentVariable("DataBaseUser");
        private static string DataBasePassword = Environment.GetEnvironmentVariable("DataBasePassword");
        private static string DataBaseName = Environment.GetEnvironmentVariable("DataBaseName");
        private static string CacheHost = Environment.GetEnvironmentVariable("CacheHost");
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RouletteContext>(opt =>
            opt.UseNpgsql($"Host = {DataBaseHost}; Database = {DataBaseName}; Username = {DataBaseUser}; Password = {DataBasePassword}"));
            services.AddControllers();
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect($"{CacheHost}"));
        }
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