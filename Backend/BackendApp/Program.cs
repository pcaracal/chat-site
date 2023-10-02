using System.Text;
using BackendApp;
using BackendApp.Interfaces;
using BackendApp.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

class Program {
  static void Main(string[] args) {
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddScoped<ILoginRepository, LoginRepository>();
    builder.Services.AddScoped<IOverviewRepository, OverviewRepository>();
    builder.Services.AddScoped<IChannelRepository, ChannelRepository>();
    builder.Services.AddScoped<IMessageRepository, MessageRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();


    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

// init.sql
    using (var db = new ApplicationDbContext(builder.Services.BuildServiceProvider()
             .GetRequiredService<DbContextOptions<ApplicationDbContext>>())) {
      db.Database.ExecuteSqlRaw(File.ReadAllText("init.sql"));
    }

    builder.Services.AddAuthentication(options => {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(o => {
      o.TokenValidationParameters = new TokenValidationParameters {
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

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
  }
}