using JWT_Auth.Data;
using JWT_Auth.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowViteFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
                                                   options.UseSqlServer(builder.Configuration.GetConnectionString("USerDatabase")));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                                  .AddJwtBearer(options =>
                                  {
                                      options.TokenValidationParameters = new TokenValidationParameters
                                      {
                                          ValidateIssuer = true,
                                          ValidateAudience = true,
                                          ValidateLifetime = true,
                                          ValidateIssuerSigningKey = true,
                                          ValidIssuer = builder.Configuration["AppSettings:Issuer"],
                                          ValidAudience = builder.Configuration["AppSettings:Audience"],
                                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Key"]!)),


                                      };
                                  });
// In builder.Services
builder.Services.AddScoped<IBookService, BookService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseCors("AllowViteFrontend");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
