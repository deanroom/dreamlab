using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;

namespace ThreadingTest
{
    public class SyncThreadPerformance
    {
        private static NetMQPoller poller = new();
        private static ConcurrentQueue<int> writeQueue = new();
        private static ConcurrentQueue<int> readQueue = new();

        public static void Run()
        {
            var number = 1000;
            poller.RunAsync();
            var task1 = Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < number; i++)
                    {
                        poller.Run(() => { writeQueue.Enqueue(i); });
                    }
                },
                TaskCreationOptions.LongRunning);
            
            Stopwatch sw = new Stopwatch();
            var task2 = Task.Factory.StartNew(() =>
            {
                var getCount = 0;
                sw.Start();
                while (true)
                {
                    if (writeQueue.TryDequeue(out var value))
                    {
                        getCount++;
                    }

                    if (getCount == number)
                        break;
                }

                Console.WriteLine($"总共耗时:{sw.ElapsedMilliseconds}ms");
            }, TaskCreationOptions.LongRunning);
            Task.WaitAll(task2);
        }

        private static void ShowThreadId(string scope)
        {
            Console.WriteLine($"{scope}:{Thread.CurrentThread.ManagedThreadId}");
        }
    }
}