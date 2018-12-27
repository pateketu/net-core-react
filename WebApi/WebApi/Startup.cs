﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.Logic;
using WebApi.Repo;

namespace WebApi
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            _logger = logger;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                // for the purpose of the exercise allowing everything!, usually not a good practice for real systems
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });
            
            services.AddMvc();         
            
            services.AddSingleton<IGameRepo, GameRepo>();
            services.AddScoped<IGameEngine, GameEngine>();
            services.AddSingleton<IRandomSeeder, RandomSeeder>();
            services.AddScoped<IRandomGenerator, RandomGenerator>();
            this._logger.LogInformation("Configured Services.");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");

            app.ConfigExceptionHandler(loggerFactory.CreateLogger("UnhandledExceptionHandler"));

            app.UseMvc();
            
        }

    }
}
