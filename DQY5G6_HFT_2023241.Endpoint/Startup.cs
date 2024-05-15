using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using DQY5G6_HFT_2023241.Repository;
using DQY5G6_HFT_2023241.Models;
using DQY5G6_HFT_2023241.Logic;
using DQY5G6_HFT_2023241.Endpoint.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace DQY5G6_HFT_2023241.Endpoint
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<GameDbContext>();

            services.AddTransient<IRepository<Developer>, DeveloperRepo>();
            services.AddTransient<IRepository<Game>, GameRepo>();
            services.AddTransient<IRepository<Launcher>, LauncherRepo>();

            services.AddTransient<IDeveloperLogic, DeveloperLogic>();
            services.AddTransient<IGameLogic, GameLogic>();
            services.AddTransient<ILauncherLogic, LauncherLogic>();

            services.AddSignalR();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DQY5G6_HFT_2023241.Endpoint", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DQY5G6_HFT_2023241.Endpoint v1"));
            }

            //app.UseExceptionHandler(c => c.Run(async context =>
            //{
            //    var exception = context.Features
            //    .Get<IExceptionHandlerPathFeature>()
            //    .Error;
            //    var response = new { Msg = exception.Message };
            //    await context.Response.WriteAsJsonAsync(response);
            //}));

            app.UseCors(x => x
                         .AllowCredentials()
                         .AllowAnyMethod()
                         .AllowAnyHeader()
                         .WithOrigins("http://localhost:43342"));

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("hub");
            });
        }
    }
}
