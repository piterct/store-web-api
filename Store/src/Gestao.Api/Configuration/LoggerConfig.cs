using Elmah.Io.Extensions.Logging;
using Gestao.Api.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Gestao.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddElmahIo(o =>
            //{
            //    o.ApiKey = "ApiKey";  Inclusao ApiKey DO ElmahIo
            //    o.LogId = new Guid(); Inclusao LogId DO ElmahIo
            //});

            services.AddHealthChecks()
               //.AddElmahIoPublisher(options =>
               //{
               //    options.ApiKey = "ApiKey"; Inclusao ApiKey DO ElmahIo
               //    options.LogId = new Guid(); Inclusao LogId DO ElmahIo
               //    options.HeartbeatId = "HeartbeatId";
               //    options.Application = "API Fornecedores e Produtos";

               //})
               .AddCheck("Produtos", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
               .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            services.AddHealthChecksUI()
                 .AddSqlServerStorage(configuration.GetConnectionString("DefaultConnection"));

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            //app.UseElmahIo();

            app.UseHealthChecks("/api/hc", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI(options =>
            {
                options.UIPath = "/api/hc-ui";
                options.ResourcesPath = $"{options.UIPath}/resources";
                options.UseRelativeApiPath = false;
                options.UseRelativeResourcesPath = false;
                options.UseRelativeWebhookPath = false;

            });

            return app;
        }
    }
}
