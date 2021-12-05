using Microsoft.EntityFrameworkCore;
using PlatformService.Data;


Console.WriteLine("********************************************************");

(new StartupConfiguration(args))
  .Start();

public class StartupConfiguration
{
  WebApplicationBuilder builder;
  public StartupConfiguration(string[] args)
  {
    builder = WebApplication.CreateBuilder(args);
  }

  public void Start()
  {
    ConfigureServices(builder.Services);
    var app = builder.Build();
    Configure(app);
    app.Run();
  }

  private void ConfigureServices(IServiceCollection services)
  {
    services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("InMem"));

    services.AddScoped<IPlatformRepo, PlatformRepo>();
    services.AddControllers();
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
  }

  private void Configure(WebApplication app)
  {

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    // app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    PrepDb.PrepPopulation(app);
  }

}