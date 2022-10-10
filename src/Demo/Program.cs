﻿// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Leonardo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");  

var stopwatch = new Stopwatch();
stopwatch.Start();

var services = new ServiceCollection();
services.AddTransient<Fibonacci>();
services.AddLogging(configure => configure.AddConsole());

IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
    .AddEnvironmentVariables().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true).Build();

var applicationSection = configuration.GetSection("Application");
var applicationConfig = applicationSection.Get<ApplicationConfig>();
services.AddDbContext<FibonacciDataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


using(var serviceProvider = services.BuildServiceProvider()) {
    var logger = serviceProvider.GetService<ILogger<Program>>();   
    logger.LogInformation($"Application Name : {applicationConfig.Name}");   
    logger.LogInformation($"Application Message : {applicationConfig.Message}");   
    var fibonacci = serviceProvider.GetService<Fibonacci>();   
    var results = await fibonacci.RunAsync(args);
    foreach (var listOfResult in results)
    {
        Console.WriteLine($"Result : {listOfResult}");
    }
}

stopwatch.Stop();

Console.WriteLine("time elapsed in seconds : " + stopwatch.Elapsed.Seconds);