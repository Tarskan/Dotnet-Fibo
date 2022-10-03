// See https://aka.ms/new-console-template for more information

using System;
using System.Diagnostics;
using Leonardo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");  

IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
    .AddEnvironmentVariables().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true).Build();

var applicationSection = configuration.GetSection("Application");
var applicationConfig = applicationSection.Get<ApplicationConfig>();

var loggerFactory = LoggerFactory.Create(builder =>
{ 
    builder.AddFilter("Microsoft", LogLevel.Warning).AddFilter("System", LogLevel.Warning).AddFilter("Demo", LogLevel.Debug).AddConsole();
});

var applicationName = configuration.GetValue<string>("Application:Name");    
var applicationMessage = configuration.GetValue<string>("Application:Message");

var logger = loggerFactory.CreateLogger("Demo.Program");

logger.LogInformation($"Application Name : {applicationConfig.Name}");
logger.LogInformation($"Application Message : {applicationConfig.Message}");

Console.WriteLine($"Application Name : {applicationName}");    
Console.WriteLine($"Application Message : {applicationMessage}");

var stopwatch = new Stopwatch();

stopwatch.Start();
await using var dataContext = new FibonacciDataContext();

var listOfResults = await new Fibonacci(dataContext).RunAsync(args);

foreach (var listOfResult in listOfResults)
{
    Console.WriteLine($"Result : {listOfResult}");
}
stopwatch.Stop();

Console.WriteLine("time elapsed in seconds : " + stopwatch.Elapsed.Seconds);