using System.Threading.Tasks;
using System;
using System.Threading;

namespace ThreadingTest
{
    public class EventThreadingTest
    {
        public static void Run()
        {
            var testClass = new ClassWithEvent();
            
            testClass.TestEvent += (sender,args)=>{
               Console.WriteLine($"Event raised by {args} with ThreadId: {Thread.CurrentThread.ManagedThreadId}");
            };
            for (int i = 0; i < 2; i++)
            {
                Task.Run(()=>{
                    testClass.RaiseEvent();
                });
            }
            for (int i = 0; i < 2; i++)
            {
                Task.Run(()=>{
                    testClass.OtherRaiseEvent();
                });
            }
            Console.ReadLine();

        }
        public class ClassWithEvent
        {
            private volatile int number=1; 
            public event EventHandler<string> TestEvent;
            public void RaiseEvent()
            {
                number *= 2;
                Console.WriteLine($"Operation*2 with ThreadId: {Thread.CurrentThread.ManagedThreadId}");
                TestEvent?.Invoke(this,"*2");
            }
             public void OtherRaiseEvent()
            {
                number += 10;
                Console.WriteLine($"Operation +2 with ThreadId: {Thread.CurrentThread.ManagedThreadId}");
                TestEvent?.Invoke(this,"+2");
            }
        }
    }   
}
