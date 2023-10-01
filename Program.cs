using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Mappings;
using NZWalksAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//For connection string
var conxString = builder.Configuration.GetConnectionString("NZWalksconx");

//For DB Context (using MySQL)
builder.Services.AddDbContext<NZWalksDbContext>(options =>
    options.UseMySql(conxString, ServerVersion.AutoDetect(conxString)));

//Injecting Repository
builder.Services.AddScoped<IRegionRepository, SqlRegionRepository>();

//Injecting Automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

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