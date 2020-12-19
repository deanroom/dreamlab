using System;
using System.Diagnostics;

namespace Timer
{
    class Program
    {
        static void Main(string[] args)
        {
            var startTime =Stopwatch.GetTimestamp(); 
            
            Console.WriteLine(startTime);
            System.Threading.Thread.Sleep(20000);
            var endTime =Stopwatch.GetTimestamp(); 
            Console.WriteLine(endTime);
            Console.WriteLine(Stopwatch.Frequency);
            Console.WriteLine((endTime-startTime)/Stopwatch.Frequency);
        }
    }
}
