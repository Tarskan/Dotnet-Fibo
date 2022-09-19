namespace Leonardo.Tests;

public class LeonardoUnitTest
{
    [Fact]
        public async Task Execute66ShouldReturn8()
        {
            var result = Fibonacci.RunAsync(new[] { "6" });
            Assert.Equal(8, result[0]);
        }
}