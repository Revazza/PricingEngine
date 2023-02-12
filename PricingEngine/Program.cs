using Microsoft.EntityFrameworkCore;
using PricingEngine.Db;
using PricingEngine.Repositories;
using PricingEngine.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PricingEngineDbContext>(options =>
{
    options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=PricingEngine");
});

builder.Services.AddTransient<IUserInputRepository, UserInputRepository>();
builder.Services.AddTransient<ICalculateUserInput, CalculateUserInput>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
