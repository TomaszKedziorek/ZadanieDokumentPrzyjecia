using System.Text.Json.Serialization;
using Api.DataAccess;
using Api.DataAccess.IRepository;
using Api.DataAccess.Repository;
using Api.Errors;
using Api.Helper;
using Api.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Api;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    string? connectionString = builder.Configuration.GetConnectionString("ApiConnectionString");
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

    builder.Services.AddAutoMapper(typeof(MappingProfiles));
    builder.Services.AddControllers();
    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
      options.InvalidModelStateResponseFactory = actionContext =>
      {
        string[]? errors = actionContext.ModelState
            .Where(e => e.Value != null && e.Value.Errors.Count > 0)
            .SelectMany(x => x.Value!.Errors)
            .Select(x => x.ErrorMessage).ToArray();
        ValidationErrorResponse errorResponse = new() { Errors = errors };
        return new BadRequestObjectResult(errorResponse);
      };
    });
    builder.Services.AddControllers().AddJsonOptions(options =>
      options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    );
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "Admission Documents API", Version = "v1" });
    });
    builder.Services.AddCors(options =>
    {
      options.AddPolicy("FrontEndClient", policyBuilder =>
        policyBuilder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins(builder.Configuration.GetSection("ClientUrl").Get<string[]>()));
    });

    var app = builder.Build();
    app.UseCors("FrontEndClient");

    DataSeeder.SeedDatabase(app);

    // Configure the HTTP request pipeline.
    app.UseMiddleware<ExceptionMiddleware>();
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Admission Documents API");
      });
    }
    app.UseStatusCodePagesWithReExecute("/errors/{0}");

    app.UseHttpsRedirection();

    app.UseAuthorization();


    app.MapControllers();

    app.Run();
  }
}
