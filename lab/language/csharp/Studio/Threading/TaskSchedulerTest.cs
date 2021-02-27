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


        private Thread[] _threads;
        private bool _disposed;
        private readonly int _threadCount = 1;
        private readonly bool _isBackground = false;
        private readonly object _lock = new object();
        public int ThreadCount => _threadCount;

        public TaskSchedulerTest()
        {
        }

        public TaskSchedulerTest(int threadCount, bool isBackground = false) : this()
        {
            if (threadCount < 1)
                throw new ArgumentOutOfRangeException(nameof(threadCount), "Must be at least 1");
            _threadCount = threadCount;
            _isBackground = isBackground;
        }

        public void RunAsync()
        {
            _threads = new Thread[_threadCount];
            for (int i = 0; i < _threadCount; i++)
            {
                _threads[i] = new Thread(Run)
                {
                    IsBackground = _isBackground,
                    Name = $"{nameof(TaskSchedulerTest)}Thread"
                };
                _threads[i].Start();
            }
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
            lock (_lock)
            {
                if (_disposed)
                    return;
                _disposed = true;
            }

            // // foreach (var thread in _threads)
            //     thread.Join();
            // _threads = null;
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