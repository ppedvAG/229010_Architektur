using ppedv.CarRentalXPress.Model.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string conString = "Server=(localdb)\\mssqllocaldb;Database=CarRentalXPress_Tests;Trusted_Connection=true";
builder.Services.AddScoped<IRepository>(x => new ppedv.CarRentalXPress.Data.EfCore.CarRentalXPressContextRepositoryAdapter(conString));


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
