using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingTest
{
    public class SynchronizationContextTest
    {
        public static void Run()
        {
            var context = new SynchronizationContext();
            Task.Run(() =>
            {
                Console.WriteLine($"[Task]This is a thread scope of {Thread.CurrentThread.ManagedThreadId}");
                context.Post(
                    (state) =>
                    {
                        Console.WriteLine(
                            $"[Task-Context-Post]This is a post operation from thread {Thread.CurrentThread.ManagedThreadId}");
                    },default);
                context.Post(
                    (state) =>
                    {
                        Console.WriteLine(
                            $"[Task-Context-Post]This is a post operation from thread {Thread.CurrentThread.ManagedThreadId}");
                    },default);
                context.Send(
                    (state) =>
                    {
                        Console.WriteLine(
                            $"[Task-Context-Send]This is a post operation from thread {Thread.CurrentThread.ManagedThreadId}");
                    },default);
                context.Send(
                    (state) =>
                    {
                        Console.WriteLine(
                            $"[Task-Context-Send]This is a post operation from thread {Thread.CurrentThread.ManagedThreadId}");
                    },default);
            });
            context.Post(
                (state) =>
                {
                    Console.WriteLine($"[Context-Post]This is a post operation from thread {Thread.CurrentThread.ManagedThreadId}");
                }, default);
            context.Post(
                (state) =>
                {
                    Console.WriteLine($"[Context-Post]This is a post operation from thread {Thread.CurrentThread.ManagedThreadId}");
                }, default);
            context.Send(
                (state) =>
                {
                    Console.WriteLine($"[Context-Send]This is a post operation from thread {Thread.CurrentThread.ManagedThreadId}");
                }, default);
            context.Send(
                (state) =>
                {
                    Console.WriteLine($"[Context-Send]This is a post operation from thread {Thread.CurrentThread.ManagedThreadId}");
                }, default);

            Console.WriteLine($"This is normal operation from thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}