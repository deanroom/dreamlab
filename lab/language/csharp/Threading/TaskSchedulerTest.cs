using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThreadingTest
{
    public class TaskSchedulerTest:TaskScheduler,IDisposable
    {
        protected override IEnumerable<Task>? GetScheduledTasks()
        {
            throw new System.NotImplementedException();
        }

        protected override void QueueTask(Task task)
        {
            throw new System.NotImplementedException();
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            throw new System.NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}