using System.Reflection;
using backend.ConsoleApps;
using backend.Contracts;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using NSwag.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

var app = builder.Build();

ConsoleApp consoleApp = new ConsoleApp();

app.MapGet("/console-app-names", () =>
{
  var assembly = Assembly.GetExecutingAssembly();
  var types = assembly.GetTypes();
  var appConsoleNames = types.Where((type) => type.BaseType == typeof(ConsoleApp))
                             .Select((type) => type.Name);
  return appConsoleNames;
});

app.MapPost("/switch-app", (Message appName) =>
{
  var assembly = Assembly.GetExecutingAssembly();
  var types = assembly.GetTypes();
  var appType = types.First(type => type.Name == appName.Content);
  consoleApp = (ConsoleApp)Activator.CreateInstance(appType)!;
  Console.WriteLine($"Switched app to {appType.Name}");
  return new Message(consoleApp.Introduce());
});

app.MapPost("/message", (Message message) =>
{
  Console.WriteLine($"Received message: {message.Content}");
  var response = consoleApp.ReceiveMessage(message.Content);
  return new Message(response);
});

app.UseCors(builder => builder
  .AllowAnyOrigin()
  .AllowAnyMethod()
  .AllowAnyHeader()
);



app.Run();
