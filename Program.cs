using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Task_Management_API.Data;
using Task_Management_API.lib;
using Task_Management_API.Service;
using Task_Management_API.Service.Auth;
using Task_Management_API.Service.Project;
using Task_Management_API.Service.Task;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(
    connectionString: connectionString,
    serverVersion: ServerVersion.AutoDetect(connectionString)
));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => 
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration.GetValue<string>("AppSettings:Issuer"),
            ValidateAudience = true,
            ValidAudience = builder.Configuration.GetValue<string>("AppSettings:Audience"),
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("AppSettings:Secret")!)
                ),
            ValidateIssuerSigningKey = true
        }
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();