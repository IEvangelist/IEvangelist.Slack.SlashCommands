using System;
using IEvangelist.Slack.SlashCommands.Configuration;
using IEvangelist.Slack.SlashCommands.Interfaces;
using IEvangelist.Slack.SlashCommands.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace IEvangelist.Slack.SlashCommands
{
    public class Startup
    {
        readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Slack - Slash Commands", Version = "v1" });
            });

            services.Configure<SlackOptions>(_configuration.GetSection(nameof(SlackOptions)));
            services.Configure<OpenWeatherMapOptions>(_configuration.GetSection(nameof(OpenWeatherMapOptions)));

            services.AddTransient<IJokeService, JokeService>();

            services.AddHttpClient<IWeatherService, WeatherService>(
                client => client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather"));
            services.AddHttpClient();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection()
               .UseSwagger()
               .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Slack, Slash Commands - API v1"));

            app.UseMvc();
        }
    }
}