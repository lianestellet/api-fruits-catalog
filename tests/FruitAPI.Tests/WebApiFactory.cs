﻿using DataAccess.Context;
using Entities.Domain;
using FruitCatalog.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Tests
{
    public class WebApiFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's FruitContext registration.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<FruitContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add a database context (FruitContext) using an in-memory database for testing.
                services.AddDbContext<FruitContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts (FruitContext).
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<FruitContext>())
                {
                    // Ensure the database is created
                    appContext.Database.EnsureCreated();
                    // Seed the database with test data

                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<FruitContext>();
                        var logger = scopedServices.GetRequiredService<ILogger<WebApiFactory>>();

                        try
                        {
                            // Seed the database with test data.
                            SeedData(db);
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, "An error occurred seeding the database with test messages. Error: {ex.Message}");
                        }
                    }
                }
            });
        }

        private void SeedData(FruitContext context)
        {
            // Add seed data to the context
            context.FruitTypes.Add(new FruitType { Name = "Citric", Description = "Like oranges" });
            context.Fruits.Add(new Fruit { Name = "Papaya", Description = "Tropical", FruitTypeId = 1 });
            context.SaveChanges();
        }
    }
}
