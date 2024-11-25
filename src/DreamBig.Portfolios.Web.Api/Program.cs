using DreamBig.Portfolios.Web.Persistent.MySQL.Helpers;

var builder = WebApplication.CreateSlimBuilder(args);
builder.WebHost.UseKestrelHttpsConfiguration();

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{environment}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();
builder.Services.AddSingleton(configuration);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});
builder.Services.AddOpenApi();

var connectionString = configuration.GetSection("MYSQL_CONNECTION").Value! ?? throw new Exception("MYSQL_CONNECTION environment variable not set");
builder.Services.AddPersistentLayer(connectionString);
builder.Services.AddHealthChecks()
    .AddMySql(connectionString);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapHealthChecks("/_health");

app.Run();