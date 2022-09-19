// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Leonardo;

var parallelOptions = new ParallelOptions();
parallelOptions.MaxDegreeOfParallelism = System.Environment.ProcessorCount;
var results = new ConcurrentBag<int>();
Parallel.ForEach(args, parallelOptions, (arg) =>
{
    var result = Fibonacci.Run(Int32.Parse(arg));
    results.Add(result);
});





