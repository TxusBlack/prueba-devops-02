using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace dotnet_app
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
            services.AddControllers();

            // Configuración de la conexión a Redis
            services.AddSingleton<IConnectionMultiplexer>(provider =>
            {
                var redisConfiguration = Configuration.GetConnectionString("Redis");

                return ConnectionMultiplexer.Connect(redisConfiguration);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var redis = app.ApplicationServices.GetRequiredService<IConnectionMultiplexer>();
                    var db = redis.GetDatabase();

                    await db.StringSetAsync("test_key", "Hello world!");
                    var value = await db.StringGetAsync("test_key");

                    await context.Response.WriteAsync($"Test connection to Redis by fetching the value of 'test_key' from .NET: {value}");
                });
            });
        }
    }
}
