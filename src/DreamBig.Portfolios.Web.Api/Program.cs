using DreamBig.Portfolios.Web.Api;
using DreamBig.Portfolios.Web.Application.Helpers;
using DreamBig.Portfolios.Web.Application.Profile.Queries;
using DreamBig.Portfolios.Web.Application.Session.Dtos;
using DreamBig.Portfolios.Web.Application.Session.Queries;
using DreamBig.Portfolios.Web.Persistent.MySQL.Helpers;
using Mediator;

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
builder.Services.AddTransient<AuthenticationMiddleware>();
builder.Services.AddApplication();
builder.Services.AddPersistentLayer(connectionString);
builder.Services.AddHealthChecks()
    .AddMySql(connectionString);
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi().AllowAnonymous();
}

app.UseHttpsRedirection();
app.UseMiddleware<AuthenticationMiddleware>();

app.MapPost("/sessions", async (SessionRequestDto request, IMediator mediator, ILogger<Program>? logger, CancellationToken cancellationToken) =>
{
    try
    {
        GetSessionQuery query = new() { UserAgent = request.UserAgent };
        var session = await mediator.Send(query, cancellationToken);
        return Results.Ok(session);
    }
    catch (Exception ex)
    {
        logger?.LogError(ex, "An error occurred while fetching session");
        return Results.Problem(
            title: "Bad Request",
            detail: ex.Message,
            statusCode: StatusCodes.Status400BadRequest
        );
    }
})
.AllowAnonymous()
.WithName("GetSession")
.WithDescription("Get session information, and the sesssion id is required for all other requests");

app.MapGet("/profile", async (IMediator mediator, ILogger<Program>? logger, CancellationToken cancellationToken) =>
{
    try
    {
        GetProfileInfoQuery query = new();
        var profile = await mediator.Send(query, cancellationToken);
        if (profile is null)
        {
            return Results.Problem(
                title: "Not Found",
                detail: "Profile information not found",
                statusCode: StatusCodes.Status404NotFound
            );
        }
        return Results.Ok(profile);
    }
    catch (Exception ex)
    {
        logger?.LogError(ex, "An error occurred while fetching profile information");
        return Results.Problem(
            title: "Bad Request",
            detail: ex.Message,
            statusCode: StatusCodes.Status400BadRequest
        );
    }
})
.WithName("GetProfile")
.WithDescription("Get profile information");

app.MapGet("/posts/{profileId}", async (string profileId, IMediator mediator, ILogger<Program>? logger, CancellationToken cancellationToken) =>
{
    try
    {
        GetPostsQuery query = new() { ProfileId = profileId };
        var profile = await mediator.Send(query, cancellationToken);
        if (profile is null)
        {
            return Results.Problem(
                title: "Not Found",
                detail: "Posts information not found",
                statusCode: StatusCodes.Status404NotFound
            );
        }
        return Results.Ok(profile);
    }
    catch (Exception ex)
    {
        logger?.LogError(ex, "An error occurred while fetching posts");
        return Results.Problem(
            title: "Bad Request",
            detail: ex.Message,
            statusCode: StatusCodes.Status400BadRequest
        );
    }
})
.WithName("GetPosts")
.WithDescription("Get posts information for a profile");

app.MapHealthChecks("/_health").AllowAnonymous();

app.Run();