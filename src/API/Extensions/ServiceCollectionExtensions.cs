using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;


namespace API.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddDatabaseConfiguration(this IServiceCollection services)
		{
			var serviceProvider = services.BuildServiceProvider();
			var configuration = serviceProvider.GetService<IConfiguration>()!;
			var dbProvider = configuration.GetSection("DatabaseProvider:Provider").Value;

			var connectionString = configuration.GetConnectionString("Postgres");

			services.AddDbContext<FruitDbContext>(options =>
			{
				switch (dbProvider)
				{
					case "PostgreSql":
						options.UseNpgsql(connectionString);
						break;
					case "SqlServer":
						options.UseSqlServer(connectionString);
						break;
					case "MySql":
						options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
						break;
					default:
						options.UseInMemoryDatabase("FruitCatalogInMemoryDb");
						break;
				}
			});
		}
	}
}
