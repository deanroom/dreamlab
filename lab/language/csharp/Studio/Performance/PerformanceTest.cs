using System.Threading;
using BenchmarkDotNet.Attributes;

namespace Performance
{
    public class PerformanceTest
    {
        [Benchmark]
        public void Run()
        {
            Thread.Sleep(1000);
        }
    }
}