var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure CORS
app.UseCors(options =>
{
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