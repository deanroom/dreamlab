using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingTest
{
    public class SynchronizationContextTest
    {
        public static void Run()
        {
            Console.WriteLine($"[Main]This is a thread scope of {Thread.CurrentThread.ManagedThreadId}");

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"[Task-Factory]This is a thread scope of {Thread.CurrentThread.ManagedThreadId}");

                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
                SynchronizationContext.Current.Post((state)=>{
                    Console.WriteLine($"[Task-Post]This is a thread scope of {Thread.CurrentThread.ManagedThreadId}");
                },1);

            },TaskCreationOptions.LongRunning);
            // Task.Run(() =>
            // {
            //     Console.WriteLine($"[Task]This is a thread scope of {Thread.CurrentThread.ManagedThreadId}");
            //     context.Post(
            //         (state) =>
            //         {
            //             Console.WriteLine(
            //                 $"[Task-Context-Post]This is a post operation in thread {Thread.CurrentThread.ManagedThreadId}");
            //         },default);
      
            //     context.Send(
            //         (state) =>
            //         {
            //             Console.WriteLine(
            //                 $"[Task-Context-Send]This is a send operation in thread {Thread.CurrentThread.ManagedThreadId}");
            //         },default);
            // });
            // context.Post(
            //     (state) =>
            //     {
            //         Console.WriteLine($"[Context-Post]This is a post operation in thread {Thread.CurrentThread.ManagedThreadId}");
            //     }, default);
            // context.Send(
            //     (state) =>
            //     {
            //         Console.WriteLine($"[Context-Send]This is a send operation in thread {Thread.CurrentThread.ManagedThreadId}");
            //     }, default);

        }
    }
}