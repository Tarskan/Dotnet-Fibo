using Leonardo;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/Fibonnaci", () => Fibonacci.RunAsync(new []{"43", "46"}));

app.Run();
