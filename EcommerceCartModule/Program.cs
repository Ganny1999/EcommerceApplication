using EcommerceCartModule.DataContext;
using EcommerceCartModule.MappingConfig;
using EcommerceCartModule.Service;
using EcommerceCartModule.Service.IService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CartDBConnection"));
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddScoped<ICustomerServiceExternal, CustomerServiceExternal>();
builder.Services.AddScoped<IProductServiceExternal, ProductServiceExternal>();

// hhtp client
builder.Services.AddHttpClient("CustomerServiceClient", u=>u.BaseAddress =new Uri(builder.Configuration["ServiceUrls:CustomerServiceClient"]));
builder.Services.AddHttpClient("ProductServiceClient", u=>u.BaseAddress =new Uri(builder.Configuration["ServiceUrls:ProductServiceClient"]));

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
