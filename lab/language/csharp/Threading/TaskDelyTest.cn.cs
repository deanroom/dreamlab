using System;
using System.Threading.Tasks;

namespace ThreadingTest
{
    public class TaskDelyTest
    {

        public static async Task Run()
        {
         
            Console.WriteLine(1);
            await Task.Delay(1000).ConfigureAwait(false);
            Console.WriteLine(2);
        }
        
    }
}