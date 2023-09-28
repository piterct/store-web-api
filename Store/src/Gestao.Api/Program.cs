using Gestao.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Environment);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

startup.Configure(app, app.Environment, provider);

app.Run();