using System.Runtime.InteropServices;
using System;
namespace NativeInop
{
    public class PInvoke
    {
        // Import the libSystem shared library and define the method
        // corresponding to the native function.
        [DllImport("libSystem.dylib")]
        private static extern int getpid(); 
        public static void Run()
        {
            // Invoke the function and get the process ID.
            int pid = getpid();
            Console.WriteLine(pid);
        }
    }
}