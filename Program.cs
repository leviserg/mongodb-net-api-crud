using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using mongodb_net_api_crud.Models;
using mongodb_net_api_crud.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var dbLogin = Environment.GetEnvironmentVariable("DEVELOPER_LOGIN") ?? string.Empty;
var dbPassword = Environment.GetEnvironmentVariable("DEVELOPER_PWD") ?? string.Empty;

var mongoSettingsSection = builder.Configuration.GetSection(nameof(MongoDbStoreSettings));

// Retrieve specific properties
string connectionString = string.Format(mongoSettingsSection.GetSection("ConnectionString").Value ?? string.Empty, dbLogin, dbPassword);

builder.Services.Configure<MongoDbStoreSettings>(mongoSettingsSection);

builder.Services.AddSingleton<IMongoDbStoreSettings>(sp =>
    sp.GetRequiredService<IOptions<MongoDbStoreSettings>>().Value
);

builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(connectionString)
);

builder.Services.AddScoped<IBookService, BookService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
