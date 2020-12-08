using System;
using System.Threading;
namespace ThreadingTest
{
    public class ManualEventResetTest
    {
        private static ManualResetEvent mre = new ManualResetEvent(false);

        public static void Run()
        {
            Console.WriteLine("\nStart 3 named threads that block on a ManualResetEvent:\n");

            for (int i = 0; i <= 2; i++)
            {
                Thread t = new Thread(ThreadProc);
                t.Name = "Thread_" + i;
                t.Start();
            }

            Thread.Sleep(500);
            Console.WriteLine("\nWhen all three threads have started, press Enter to call Set()" +
                              "\nto release all the threads.\n");
            Console.ReadLine();

            mre.Set();

            Thread.Sleep(500);
            Console.WriteLine("\nWhen a ManualResetEvent is signaled, threads that call WaitOne()" +
                              "\ndo not block. Press Enter to show this.\n");
            Console.ReadLine();

            for (int i = 3; i <= 10; i++)
            {
                Thread t = new Thread(ThreadProc);
                t.Name = "Thread_" + i;
                t.Start();
            }

            Thread.Sleep(500);
            Console.WriteLine("\nPress Enter to call Reset(), so that threads once again block" +
                              "\nwhen they call WaitOne().\n");
            Console.ReadLine();

            mre.Reset();

            Thread.Sleep(500);
            Console.WriteLine("\nWhen a ManualResetEvent is non-signaled, threads that call WaitOne()" +
                              "\nwill all block. Press Enter to show this.\n");
            Console.ReadLine();

            for (int i = 12; i <= 15; i++)
            {
                Thread t = new Thread(ThreadProc);
                t.Name = "Thread_" + i;
                t.Start();
            }

            Thread.Sleep(500);
            Console.WriteLine("\nPress Enter to call Set(), so that threads do not block" +
                              "\nwhen they call WaitOne().\n");
            Console.ReadLine();

            mre.Set();

            
            Thread.Sleep(500);
            Console.WriteLine("\nPress Enter to call Reset(), so that threads block" +
                              "\nwhen they call WaitOne().\n");
            Console.ReadLine();


            mre.Reset();

            // Start a thread that waits on the ManualResetEvent.
            Thread t5 = new Thread(ThreadProc);
            t5.Name = "Thread_";
            t5.Start();

            Thread.Sleep(500);
            Console.WriteLine("\nPress Enter to call Set() and conclude the demo.");
            Console.ReadLine();

            mre.Set();

            // If you run this example in Visual Studio, uncomment the following line:
            //Console.ReadLine();
        }

        private static void ThreadProc()
        {
            string name = Thread.CurrentThread.Name;

            Console.WriteLine(name + " starts and calls mre.WaitOne()");

            mre.WaitOne();

            Console.WriteLine(name + " ends.");
        }
    }
}