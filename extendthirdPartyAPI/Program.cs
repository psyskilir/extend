using System.Text.Json.Serialization;
using extendthirdPartyAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opts =>
{
   // var enumConverter = new JsonStringEnumConverter();
   // opts.JsonSerializerOptions.Converters.Add(enumConverter);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();


builder.Services.AddScoped<IPayExtendConnector, PayExtendConnectorImpl>();
builder.Services.AddScoped<ITokenManager, TokenManagerImpl>();
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

