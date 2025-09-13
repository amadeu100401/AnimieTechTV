using AnimieTechTv.API.Filters;
using AnimieTechTv.Application;
using AnimieTechTv.Infrastructure;
using AnimieTechTv.Infrastructure.Extensions;
using AnimieTechTv.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructure(builder.Configuration);


builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.WebHost.ConfigureKestrel(options =>
{ 
    options.ListenAnyIP(8080);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDatabase();

app.Run();

void MigrateDatabase()
{
    var configuration = builder.Configuration;

    var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();


    DatabaseMigration.Migrate(configuration.ConnectionString(), serviceScope.ServiceProvider);
}
