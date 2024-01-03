using Microsoft.EntityFrameworkCore;
using NftShirt.Server.Data;
using NftShirt.Server.Infra.IRepositories;
using NftShirt.Server.Infra.Repositories;
using NftShirtApi.Infra.Blockchain;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options => {
    options.ListenLocalhost(5000);
});

builder.Services.AddDbContext<NftShirtContext>(options => {
    options.UseNpgsql("Host=localhost;Port=5432;Database=NftShirt;Username=postgres;Password=3309;Include Error Detail=true");
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICollectionRepository, CollectionRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IItenRepository, ItenRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INftagRepository, NftagRepository>();
builder.Services.AddScoped<INftRepository, NftRepository>();
builder.Services.AddScoped<IPollygonContractService, PollygonContractService>();
builder.Services.AddScoped<IPollygonNftService, PollygonNftService>();


builder.Services.AddControllers();
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
