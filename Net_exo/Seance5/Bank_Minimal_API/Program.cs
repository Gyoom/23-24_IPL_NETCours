using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// GET /TVA


app.MapGet("/tva{country}", (
    [FromRoute] string country,
    [FromQuery(Name = "price")] int price) =>
{

    if (country.Equals("BE"))
    {
        return price * 1.1;
    }
    if (country.Equals("FR"))
    {
        return price * 1.5;
    }
    return 0;
});

app.Run();