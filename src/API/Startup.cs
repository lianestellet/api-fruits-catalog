using API.Extensions;
using BusinessLogic.Mappings;
using BusinessLogic.Services;
using DataAccess.Context;
using DataAccess.Repositories;

namespace API
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
            services.AddDatabaseConfiguration();
            services.AddAutoMapper(typeof(BusinessLogicMappingProfile));

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<IFruitService, FruitService>();
            services.AddScoped<IFruitRepository, FruitRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FruitCatalog API V1");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            InitializeDatabase(app);
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<FruitDbContext>();
            InitializeDbContext.Seed(context);
        }
    }
}