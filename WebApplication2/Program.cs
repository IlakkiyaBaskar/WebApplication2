using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .WriteTo.Console()
        .WriteTo.File("logs/LogsInformation.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

builder.Host.UseSerilog(); // Integrates Serilog with the .NET logging infrastructure

// Add services to the container.
//builder.Services.AddSingleton<Microsoft.Extensions.Logging.ILogger, Log>();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOriginAndHeader",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
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

app.UseCors("AllowAnyOriginPolicy");


app.UseHttpsRedirection();
app.UseCors("AllowAnyOriginPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
