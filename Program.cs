using Prometheus; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
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

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});

//app.UseHttpsRedirection();
app.UseAuthorization();
app.Urls.Add("http://*:8080"); // Allow the app to bind to port 8080

// Add Prometheus metrics middleware
app.UseHttpMetrics(); // Captures HTTP-related metrics

// Expose /metrics endpoint for Prometheus
app.MapMetrics();

app.MapControllers();
app.Run();
