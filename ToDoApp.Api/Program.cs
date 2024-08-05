using Microsoft.EntityFrameworkCore;
using ToDoApp.Api.Data;
using ToDoApp.Api.Helpers;
using ToDoApp.Api.Mapping;
using ToDoApp.Api.Repositories;
using ToDoApp.Api.Services;

var builder = WebApplication.CreateBuilder(args);

#region === Configuration ===

ConfigureAppSettings(builder.Configuration, builder.Environment);
string DefaultConnection = builder.Configuration.GetConnectionString(nameof(DefaultConnection)) 
	?? throw new InvalidOperationException("DefaultConnection configuration section is missing or invalid.");

#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add CORS Policy
builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy",
		builder => builder.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader());
});
// Add DbContext connection
builder.Services.AddDbContext<ToDoAppDbContext>(options =>
	options.UseSqlServer(DefaultConnection)
);
builder.Services.AddAutoMapper(typeof(MappingProfile));

#region === Dependency Injection ===

#region === Repositories ===
builder.Services.AddScoped<IGoalRepository, GoalRepository>();
builder.Services.AddScoped<ISubTaskRepository, SubTaskRepository>();
#endregion

#region === Services ===
builder.Services.AddScoped<IGoalService, GoalService>();
builder.Services.AddScoped<ISubTaskService, SubTaskService>();
#endregion

#region === Helpers ===
builder.Services.AddScoped<GoalProgressHelper>();
#endregion

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

#region === Swagger ===
app.UseSwagger();
app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint(url: $"/swagger/v1/swagger.json", name: "ToDoApp");
	c.RoutePrefix = "swagger";
});
#endregion

app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
static void ConfigureAppSettings(IConfigurationBuilder builder, IWebHostEnvironment environment)
{
	builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

	if (!string.IsNullOrEmpty(environment.EnvironmentName))
	{
		var envFileName = $"appsettings.{environment.EnvironmentName}.json";
		builder.AddJsonFile(envFileName, optional: true, reloadOnChange: true);
	}

	foreach (var currentFile in Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.json"))
	{
		if (!currentFile.EndsWith($"appsettings.{environment.EnvironmentName}.json", StringComparison.OrdinalIgnoreCase) &&
			!currentFile.EndsWith("appsettings.json", StringComparison.OrdinalIgnoreCase))
		{
			builder.AddJsonFile(currentFile, optional: true, reloadOnChange: true);
		}
	}
}