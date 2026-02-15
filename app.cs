#:sdk Microsoft.NET.Sdk.Web
// 👆 This line pulls in the Web API SDK

Console.WriteLine("App starting");

// 👇 Now configure and start the web server
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!"); // 👈 A single endpoint

app.Run();
