using MarvellousApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
builder.Configuration.SetBasePath(env.ContentRootPath);
builder.Configuration.AddJsonFile("appsettings.json", true, true);
builder.Configuration.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
builder.Configuration.AddEnvironmentVariables();


// Add services to the container.
builder.Services.AddMarvellousApiExtensions(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();