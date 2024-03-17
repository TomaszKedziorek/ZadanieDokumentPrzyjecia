using Bogus;
using Api.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.DataAccess;

public static class DataSeeder
{

  public static async void SeedDatabase(WebApplication app)
  {
    using (var scope = app.Services.CreateScope())
    {
      var services = scope.ServiceProvider;
      var loggerFactory = services.GetRequiredService<ILoggerFactory>();
      try
      {
        var dbContext = services.GetRequiredService<AppDbContext>();
        if (dbContext.Database.GetPendingMigrations().Any())
        {
          await dbContext.Database.MigrateAsync();
        }
        await SeedAsync(dbContext, loggerFactory);
      }
      catch (Exception e)
      {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(e, "An error occured during migrations");
      }
    }
  }

  private static async Task SeedAsync(AppDbContext dbContext, ILoggerFactory loggerFactory)
  {
    var locale = "en";
    Randomizer.Seed = new Random();
    try
    {
      if (!dbContext.Labels.Any())
      {
        await dbContext.Labels.AddRangeAsync(GenerateLabel(4));
        await dbContext.SaveChangesAsync();
      }

      if (!dbContext.AdmissionDocuments.Any())
      {
        List<AdmissionDocument> docs = new();
        var lables = await dbContext.Labels.ToListAsync();

        for (int i = 1; i < 7; i++)
        {
          AdmissionDocument doc = new()
          {
            TargetWarehouse = GenerateWarehouse(),
            Supplier = GenerateSupplier(),
            Labels = lables.FindAll(x => x.Id == i % 3),
            CommodityList = GenerateComodity(i % 2 == 0 ? 2 : 1)
          };
          docs.Add(doc);
        }
        await dbContext.AdmissionDocuments.AddRangeAsync(docs);
        await dbContext.SaveChangesAsync();
      }
    }
    catch (Exception e)
    {
      var loggger = loggerFactory.CreateLogger<AppDbContext>();
      loggger.LogError(e.Message);
    }
  }

  private static List<Commodity> GenerateComodity(int num, string locale = "en")
  {
    var generator = new Faker<Commodity>(locale)
        .RuleFor(a => a.Name, f => f.Commerce.Product())
        .RuleFor(a => a.Code, f => f.Hacker.Random.AlphaNumeric(5))
        .RuleFor(a => a.Quantity, f => f.Random.Int(1, 100))
        .RuleFor(a => a.Price, f => Math.Round(f.Random.Double(1, 12453), 2));
    return generator.Generate(num).ToList();
  }

  private static Address GenerateAddress(string locale = "en")
  {
    var generator = new Faker<Address>(locale)
        .RuleFor(a => a.City, f => f.Address.City())
        .RuleFor(a => a.State, f => f.Address.State())
        .RuleFor(a => a.Country, f => f.Address.Country())
        .RuleFor(a => a.ZipCode, f => f.Address.ZipCode())
        .RuleFor(a => a.Street, f => f.Address.StreetName());
    return generator.Generate();
  }

  private static Supplier GenerateSupplier(string locale = "en")
  {
    var generator = new Faker<Supplier>(locale)
        .RuleFor(a => a.CompanyName, f => f.Company.CompanyName())
        .RuleFor(a => a.Address, GenerateAddress());
    return generator.Generate();
  }

  private static List<Label> GenerateLabel(int num, string locale = "en")
  {
    var generator = new Faker<Label>(locale)
        .RuleFor(a => a.Name, f => f.Lorem.Word());
    return generator.Generate(num).ToList();
  }
  private static Warehouse GenerateWarehouse(string locale = "en")
  {
    var generator = new Faker<Warehouse>(locale)
        .RuleFor(a => a.Name, f => f.Company.CompanyName() + " Warehouse")
        .RuleFor(a => a.Symbol, f => f.Company.CompanySuffix());
    return generator.Generate();
  }
}
