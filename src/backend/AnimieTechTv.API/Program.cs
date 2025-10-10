    using AnimieTechTv.API.Filters;
    using AnimieTechTv.Application;
    using AnimieTechTv.Infrastructure;
    using AnimieTechTv.Infrastructure.Extensions;
    using AnimieTechTv.Infrastructure.Migrations;
    using Serilog;
    using Serilog.Events;

    var builder = WebApplication.CreateBuilder(args);

    // Serviços 
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Application & Infrastructure
    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructure(builder.Configuration);

    // Exception Filter
    builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

    // Logging
    var logPath = Path.Combine(AppContext.BaseDirectory, "logs");
    if (!Directory.Exists(logPath))
    {
        Directory.CreateDirectory(logPath);
    }

    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Error()
        .Enrich.FromLogContext()
        .Filter.ByExcluding(logEvent => logEvent.Properties.ContainsKey("Password"))
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
        .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Error)
        .WriteTo.File(
            Path.Combine(logPath, "log-.txt"),
            rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: 30,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
            restrictedToMinimumLevel: LogEventLevel.Error
        )
        .CreateLogger();

    builder.Services.AddSingleton(Log.Logger);
    builder.Host.UseSerilog();

    // CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSwagger", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });

    // Kestrel 
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(8080); // HTTP na porta 8080
    });

    var app = builder.Build();

    // Pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "AnimieTechTv API v1");
            c.RoutePrefix = string.Empty;
        });
    }

    // Aplicar CORS
    app.UseCors("AllowSwagger");

    app.UseAuthorization();

    app.MapControllers();

    // Database Migration
    MigrateDatabase();

    app.Run();

    // Método de Migrations
    void MigrateDatabase()
    {
        var logger = app.Services.GetRequiredService<ILogger<DatabaseMigration>>();
        var configuration = builder.Configuration;
        var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

        DatabaseMigration.Migrate(configuration.ConnectionString(), serviceScope.ServiceProvider, logger);
    }
