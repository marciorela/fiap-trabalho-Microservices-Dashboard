using Geekburger.Dashboard.Data;
using Geekburger.Dashboard.Database;
using Geekburger.Dashboard.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DashboardDbContext>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<RestrictionRepository>();
builder.Services.AddScoped<RestrictionService>();
builder.Services.AddScoped<SalesService>();

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
