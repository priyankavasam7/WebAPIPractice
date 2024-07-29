using CustomersAPI.Data;
using CustomersAPI.Middleware;
using CustomersAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddDbContext<CustomerDbContext>(options =>
{
    options.UseInMemoryDatabase("CustomerDb");
});
//builder.Services.AddDbContext<CustomerDbContext>(options =>
//{
//    string connectionString = builder.Configuration.GetConnectionString("CustomerConnectionString");
//    options.UseSqlServer(connectionString);
//});

var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseApiKeyAuthMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
