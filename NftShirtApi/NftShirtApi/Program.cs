using Microsoft.EntityFrameworkCore;
using NftShirt.Server.Data;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options => {
    options.ListenLocalhost(5000);
});
builder.Services.AddDbContext<NftShirtContext>(options => {
    options.UseNpgsql("Host=localhost;Port=5432;Database=Cartsy;Username=postgres;Password=3309;Include Error Detail=true");
});
// Add services to the container.

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
