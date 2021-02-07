using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;

namespace ThreadingTest
{
    public class Message
    {
        public int Id { get; set; }
        public long Start { get; set; }
        public long End { get; set; }
    }

    public class SyncThreadPerformance
    {
        private static NetMQPoller poller = new();
        private static ConcurrentQueue<Message> writeQueue = new();
        private static ConcurrentQueue<Message> readQueue = new();

        public static void Run()
        {
            var number = 100;
            poller.RunAsync();
            var task1 = Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < number; i++)
                    {
                        poller.Run(() =>
                        {
                            writeQueue.Enqueue(new Message() {Id = i, Start = Stopwatch.GetTimestamp()});
                        });
                    }
                },
                TaskCreationOptions.LongRunning);

            var task2 = Task.Factory.StartNew(() =>
            {
                var getCount = 0;
                while (true)
                {
                    if (writeQueue.TryDequeue(out var value))
                    {
                        value.End = Stopwatch.GetTimestamp();
                        readQueue.Enqueue(value);
                        getCount++;
                    }

                    if (getCount == number)
                        break;
                }
            }, TaskCreationOptions.LongRunning);
            Task.WaitAll(task2);
            foreach (var message in readQueue)
            {
                Console.WriteLine($"总共耗时:{(message.End - message.Start).GetMicroSeconds()}µs");
            }
        }

        private static void ShowThreadId(string scope)
        {
            Console.WriteLine($"{scope}:{Thread.CurrentThread.ManagedThreadId}");
        }
    }

    public static class Extensions
    {
        public static long GetMicroSeconds(this long timeStamp)
        {
            Console.WriteLine(Stopwatch.Frequency );
            return timeStamp / (Stopwatch.Frequency / 1_000_000);
        }
    }
}