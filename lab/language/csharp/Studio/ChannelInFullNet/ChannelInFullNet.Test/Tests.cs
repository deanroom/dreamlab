using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace ChannelInFullNet.Test
{
    public class Test_Channel
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Test_Channel(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test_Request_Response()
        {
            var channel = new Channel(
                new ConcurrentQueue<Message>(),
                new ConcurrentQueue<Message>(),
                new ConcurrentQueue<PubMessage>(),
                new ConcurrentQueue<Action>());
            using (var client = new Client(channel))
            using (var server = new Server(channel))
            {
                ShowThread("Outer");
                server.TaskExecute += message => new OutMessage() {Id = 2, Name = "Outmessage"};
                client.SubscribedDataReceived += (sender, message) =>
                {
                    ShowThread("SubscribedDataReceived");
                    _testOutputHelper.WriteLine(message.Topic);
                };
                server.RunAsync();

                for (int i = 0; i < 1000; i++)
                {
                    var result = client.Request<InMessage, OutMessage>(new InMessage() {Id = 1, Name = "InMessage"});
                    Assert.True(result.Id == 2);
                    ShowThread("Client Request && Execute"); 
                    server.Execute(() => { ShowThread("ServerExecute"); });
                    Thread.Sleep(1000);
                }

                Thread.Sleep(1000);
            }
        }

        private void ShowThread(string scope)
        {
            _testOutputHelper.WriteLine($"{scope}- Thread Id is :{Thread.CurrentThread.ManagedThreadId}");
        }
    }
}