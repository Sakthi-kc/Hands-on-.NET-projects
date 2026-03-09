using Microsoft.EntityFrameworkCore;
using Product_Management.Configuration;
using Product_Management.Data;
using Product_Management.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ProductDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString"));
});


builder.Services.AddAutoMapper(typeof(ProductAutoMapper));


builder.Services.AddScoped(typeof(IZenRepo<>),typeof(ZenRepo<>));
builder.Services.AddScoped<IProductRepo, ProductRepo>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
