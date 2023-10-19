using Serilog;
using SoftwareControle.WebUi.Configuration;
using System.Text.Json.Serialization;
using WebUi.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services
	.AddApplicationDbContext(config)
	.AddApplicationDependencyInjection()
	.AddAuthenticationAndAuthorization(config)
	.AddCorsPolicy();

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, configuration) =>
	configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseCors("OpenCorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
