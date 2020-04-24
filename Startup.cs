using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using customercrud.Data;
using customercrud.Data.Interfaces;
using customercrud.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace customercrud
{
    public class Startup
    {
        private static Task WriteResponse(HttpContext httpContext, HealthReport result)
        {
            httpContext.Response.ContentType = "application/json";

            var json = new JObject(
                new JProperty("status", result.Status.ToString().ToLowerInvariant()),
                new JProperty("results", new JObject(result.Entries.Select(pair =>
                    new JProperty(pair.Key, new JObject(
                        new JProperty("status", pair.Value.Status.ToString().ToString().ToLowerInvariant()),
                        new JProperty("description", pair.Value.Description),
                        new JProperty("data", new JObject(pair.Value.Data.Select(
                            p => new JProperty(p.Key, p.Value))))))))));

            return httpContext.Response.WriteAsync(
                json.ToString(Formatting.Indented));
        }
            
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<DatabaseSettings>(options => Configuration.GetSection("DatabaseSettings").Bind(options));

            // Repositories
            var dataBaseSettings = services.BuildServiceProvider().GetService<IOptions<DatabaseSettings>>().Value;
            services.AddSingleton<ICustomerRepository>(provider => {
                var mongoDatabase = new MongoClient(dataBaseSettings.ConnectionString).GetDatabase(dataBaseSettings.Database);
                return new CustomerRepository(mongoDatabase);
            });
            
            services.AddHealthChecks()
                .AddMongoDb(dataBaseSettings.ConnectionString, timeout: TimeSpan.FromSeconds(5))
                .AddUrlGroup(new Uri("https://api.mundipagg.com/merchant/v1"), timeout: TimeSpan.FromSeconds(5));
                

            services.AddMvc(options => options.EnableEndpointRouting = false);
                
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddMvcCore().AddApiExplorer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var options = new HealthCheckOptions
                {
                    AllowCachingResponses = true,
                    ResponseWriter = WriteResponse
                };

            app.UseHealthChecks("/v1/healthcheck", options);

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api-docs/v1/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api-docs/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
