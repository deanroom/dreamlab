using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingTest
{
    public class SynchronizationContextTest
    {

        [STAThread]
        public static void Run()
        {
            Console.WriteLine($"[Main]This is a thread scope of {Thread.CurrentThread.ManagedThreadId}");

            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            var context = SynchronizationContext.Current;
            context.Post((state) =>
            {
                Console.WriteLine($"[Context-Poist]This is a thread scope of {Thread.CurrentThread.ManagedThreadId}");
            }, 1);
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"[Task-Factory]This is a thread scope of {Thread.CurrentThread.ManagedThreadId}");

                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

            },CancellationToken.None, TaskCreationOptions.LongRunning,TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}