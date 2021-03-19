using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;

namespace MobileGateway.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                //var descriptor = services.SingleOrDefault(
                //    d => d.ServiceType ==
                //        typeof(DbContextOptions<ApplicationDbContext>));
                //services.Remove(descriptor);
                //services.AddDbContext<ApplicationDbContext>(options =>
                //{
                //    options.UseInMemoryDatabase("InMemoryDbForTesting");
                //});

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    //var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                    //db.Database.EnsureCreated();
                    //try
                    //{
                    //    Utilities.InitializeDbForTests(db);
                    //}
                    //catch (Exception ex)
                    //{
                    //    logger.LogError(ex, "An error occurred seeding the " +
                    //        "database with test messages. Error: {Message}", ex.Message);
                    //}
                }
            });

            builder.CaptureStartupErrors(true);
            builder.Configure(app => 
            {
                var logger = app.ApplicationServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                LogMiddlleware(app, logger);
            });
        }

        private void LogMiddlleware(IApplicationBuilder app, ILogger logger)
        {
            app.Use(async (context, next) =>
            {
                // Do work that doesn't write to the Response.
                logger.LogInformation($"**** ENTER {context.Request.Path}");

                await next();

                // Do other work that doesn't write to the Response.
                logger.LogInformation($"**** EXIT {context.Request.Path}");
            });
        }
    }
}
