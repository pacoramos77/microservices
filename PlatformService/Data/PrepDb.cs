using Microsoft.AspNetCore.Builder;
using PlatformService.Models;

namespace PlatformService.Data
{
  public class PrepDb
  {
    public static void PrepPopulation(IApplicationBuilder app, bool isProd)
    {
      using (var serviceScope = app.ApplicationServices.CreateScope())
      {

        var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
        SeedData(context!, isProd);
      }
    }

    private static void SeedData(AppDbContext context, bool isProd)
    {
      if (isProd)
      {
        try
        {

          Console.WriteLine("--> Attempting to apply migrations...");
          context.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
          Console.WriteLine($"--> Could not run migrations: {ex.Message}");
        }
      }

      if (!context!.Platforms.Any())
      {
        Console.WriteLine("--> Seeding data...");
        context.Platforms.AddRange(
          new Platform { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
          new Platform { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
          new Platform { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
        );

        context.SaveChanges();

      }
      else
      {
        Console.WriteLine("--> We already have data");
      }
    }
  }
}
