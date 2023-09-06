using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
  o.TokenValidationParameters = new TokenValidationParameters
  {
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey
      (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = false,
    ValidateIssuerSigningKey = true
  };
});
builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Auth
app.UseAuthentication();
app.UseAuthorization();

// Configure CORS
app.UseCors(options => {
  options.AllowAnyOrigin(); // Allow requests from any origin (IP address)
  options.AllowAnyMethod(); // Allow any HTTP method (GET, POST, PUT, etc.)
  options.AllowAnyHeader(); // Allow any HTTP headers
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}

// REMOVE LATER, tests auth
app.MapGet("/get", () => "Auth!").RequireAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();