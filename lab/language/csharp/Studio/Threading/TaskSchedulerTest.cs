using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingTest
{
    public class TaskSchedulerTest : TaskScheduler, IDisposable
    {
        private readonly ConcurrentQueue<Task> _queue = new();
        private bool _disposed;

        public TaskSchedulerTest()
        {
        }

        public Task Execute(Action action) =>
            Task.Factory.StartNew(action,
                CancellationToken.None,
                TaskCreationOptions.HideScheduler, this);

        public void Run()
        {
            while (true)
            {
                while (_queue.TryDequeue(out var task))
                {
                    if (task == null)
                        return;
                    TryExecuteTask(task);
                }
            }
        }

        protected override IEnumerable<Task> GetScheduledTasks() => _queue.ToArray();

        protected override void QueueTask(Task task)
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(TaskSchedulerTest).FullName);
            if (task == null)
                throw new ArgumentNullException(nameof(task));
            try
            {
                _queue.Enqueue(task);
            }
            catch (ObjectDisposedException e)
            {
                throw new ObjectDisposedException(typeof(TaskSchedulerTest).FullName, e);
            }
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(TaskSchedulerTest).FullName);
            return !taskWasPreviouslyQueued && TryExecuteTask(task);
        }

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;
        }
    }

    public class TestScheduler
    {
        public static void Run()
        {
            Console.WriteLine($"Current Thread Id:{Thread.CurrentThread.ManagedThreadId}");

            using var scheduler = new TaskSchedulerTest();
            var thread = new Thread(() =>
            {
                Console.WriteLine($"Current Thread Id:{Thread.CurrentThread.ManagedThreadId}");
                scheduler.Run();
            });
            thread.Start();
            scheduler.Execute(() =>
            {
                Console.WriteLine($"Current Thread Id:{Thread.CurrentThread.ManagedThreadId}");

                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Current Thread Id:{Thread.CurrentThread.ManagedThreadId}");
                });
            });
            thread.Join();
        }
    }
}