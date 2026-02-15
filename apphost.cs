#:sdk Aspire.AppHost.Sdk@13.1.1
#:package Aspire.Hosting.Yarp@*

using Aspire.Hosting.Yarp;

var builder = DistributedApplication.CreateBuilder(args);

#pragma warning disable ASPIRECSHARPAPPS001
var app = builder
    .AddCSharpApp("api", "./app.cs")
    .WithHttpEndpoint(port: 5000, targetPort: 5000, isProxied: false);

builder
    .AddYarp("reverse-proxy")
    .WithHostPort(8080)
    .WithConfiguration(yarp =>
    {
        yarp.AddRoute(app).WithMatchHosts("localhost", "api.local");
    });

builder.Build().Run();

// Dump the aspire config
// dotnet run ./apphost.cs --publisher manifest --output-path aspire-manifest.json
