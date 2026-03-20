using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using Tango.Employee.Configuration;
using Tango.Employee.Data;
using Tango.Employee.Data.Repositories;
using Tango.Employee.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add service for JWT authentication
var key = Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWTSecretKey"));

var aud = builder.Configuration.GetValue<string>("JWTAudience");
var issuer = builder.Configuration.GetValue<string>("JWTIssuer");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidateAudience = true,
        ValidAudience = aud,
        ValidateIssuer = true,
        ValidIssuer = issuer,
    };
});

builder.Services.AddControllers()
    //to support xml and json response
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();

builder.Services.AddDbContext<TangoDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnectionString"));
});

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddScoped(typeof(ITangoRepo<>), typeof(TangoRepo<>));
builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

//To add Authorize in Swagger
builder.Services.AddSwaggerGen(options =>
{
    //adds authorize button to swagger page
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "JWT Authorization",
        //type and scheme are required else won't show text box
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        In = ParameterLocation.Header,
        Description = "Please paste the token in the text box"
    });

    //adds lock to all the APIs
    options.AddSecurityRequirement((document) => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

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

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        var exception = contextFeature?.Error;

        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(new
        {
            StatusCode = 500,
            Message = exception?.Message
        });
    });
});

app.Run();
