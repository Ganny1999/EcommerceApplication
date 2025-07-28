using EcommerceOrderModule.DataContext;
using EcommerceOrderModule.Service;
using EcommerceOrderModule.Service.IExternalService;
using EcommerceOrderModule.Service.Iservice;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options=>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDBConnection"));
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IOrderService,OrderService>();
builder.Services.AddScoped<ICartServiceExternal,CartServiceExternal>();

builder.Services.AddHttpClient("CartServiceClient", u => u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CartServiceClient"]));

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
