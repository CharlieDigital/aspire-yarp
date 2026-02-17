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
    // .WithHostPort(80) // 👈 Doesn't work because this is a privileged port; only `sudo dotnet run` can bind to it.
    .WithConfiguration(yarp =>
    {
        yarp.AddRoute(app).WithMatchHosts("localhost", "api.localhost");
    });

builder.Build().Run();

// https://github.com/dotnet/aspire/issues/5508
// Dump the aspire config
// dotnet run ./apphost.cs --publisher manifest --output-path aspire-manifest.json
