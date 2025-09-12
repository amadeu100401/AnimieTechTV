using AnimieTechTv.API.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddMediatR(typeof(Program));

builder.Services.AddFluentMigrator(builder.Configuration);  

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});

var dbInitializer = new DbInitializer(builder.Configuration);

await dbInitializer.EnsureDataBaseAsync();

var app = builder.Build();

FluentMigratiorConfig.MigrateDatabase(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
