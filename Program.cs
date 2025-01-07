using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger in all environments
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    c.RoutePrefix = string.Empty; // Serve Swagger UI at root URL
});

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});

// app.UseHttpsRedirection();  // Disabled for direct IP access
app.UseAuthorization();
app.UseHttpMetrics(); // Prometheus middleware
app.MapMetrics();     // Expose /metrics endpoint for Prometheus
app.MapControllers();
app.Run();
