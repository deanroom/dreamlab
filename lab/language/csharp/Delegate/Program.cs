using System;

namespace Delegate
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new DelegateServer();
            var client= new DelegateClient(server);
            var client2= new DelegateClient(server);
            var client3= new DelegateClient(server);
            var client4= new DelegateClient(server);
            var client5= new DelegateClient(server);
            server.Add();
            Console.WriteLine(server.Number.NumberValue);
        }
    }
}