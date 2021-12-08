using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

(new StartupConfiguration(args))
  .Start();

public class StartupConfiguration
{
  readonly WebApplicationBuilder _builder;
  readonly IWebHostEnvironment _env;
  readonly ConfigurationManager _configuration;

  public StartupConfiguration(string[] args)
  {
    _builder = WebApplication.CreateBuilder(args);
    _env = _builder.Environment;
    _configuration = _builder.Configuration;
  }

  public void Start()
  {
    ConfigureServices(_builder.Services);

    var app = _builder.Build();

    Console.WriteLine($"--> CommandService Endpoint {app.Configuration["CommandService"]}");
    Configure(app);
    app.Run();
  }

  private void ConfigureServices(IServiceCollection services)
  {
    if (_env.IsProduction())
    {
      services.AddDbContext<AppDbContext>(options =>
          options.UseSqlServer(
            _configuration.GetConnectionString("Platforms")));
    }
    else
    {
      Console.WriteLine("--> Using InMemory Database");
      services.AddDbContext<AppDbContext>(options =>
          options.UseInMemoryDatabase("InMem"));
    }

    services.AddScoped<IPlatformRepo, PlatformRepo>();
    services.AddHttpClient<ICommandDataClient, HttpCommandDataclient>();

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



    PrepDb.PrepPopulation(app, _env.IsProduction());
  }

}