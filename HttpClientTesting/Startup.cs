using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpClientTesting.Client;
using HttpClientTesting.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

namespace HttpClientTesting
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HttpClientTesting", Version = "v1" });
            });
            
            services.AddHttpClient("discovery", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://discovery.googleapis.com");
                httpClient.DefaultRequestHeaders.Add(
                    HeaderNames.UserAgent, "HttpRequestsSample");
            });

            // registers the http Client as a singleton.
            services.AddSingleton<ISimpleHttpClient, SimpleHttpClient>();

            // register the service.
            services.AddScoped<ISimpleService, SimpleService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HttpClientTesting v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}