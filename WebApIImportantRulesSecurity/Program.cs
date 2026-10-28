using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApIImportantRulesSecurity.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Required for OpenAPI metadata
builder.Services.AddSwaggerGen();           // Generates the Swagger documentation

// 1. ADD THIS: Define a CORS policy that allows localhost connections
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "MyApi",
            ValidAudience = "MyClient",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("lOvU5X/NtDdZuTflPmfXIcrRaZXUE/pvxm9yM8IdUAoUfmygu3SdcLpme6Dv017s5ow4zanqiLtXULxFezTYUw=="))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseMiddleware<WebApIImportantRulesSecurity.Middleware.ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   // Serves the generated JSON endpoint
    app.UseSwaggerUI(); // Serves the visual Swagger interactive UI
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

// 2. ADD THIS: Enable CORS middleware (Must be before UseAuthorization and MapControllers)
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
