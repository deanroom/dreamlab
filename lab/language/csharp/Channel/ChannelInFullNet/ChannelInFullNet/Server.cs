using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChannelInFullNet
{
    public class Server : IServer
    {
        private readonly Channel _channel;
        private bool _isRunning;
        private Timer _timer;
        private Task _coreTask;

        public event Func<InMessage, OutMessage> TaskExecute;

        public Server(Channel channel)
        {
            _channel = channel;
        }

        private void TimerLoop()
        {
            _channel.WritePub(new PubMessage()
            {
                Topic = "PubMessage"
            });
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        public void Connect(string requestAddress, string subAddress)
        {
        }

        public void Execute(Action action)
        {
            _channel.Execute(action);
        }

        public void Run()
        {
        }

        public void RunAsync()
        {
            _isRunning = true;
            _coreTask = Task.Factory.StartNew(() =>
                {
                    _timer = new Timer((state) => { TimerLoop(); }, 1, TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(1));

                    while (_isRunning)
                    {
                        var message = _channel.ReadUp();
                        if(message==null)
                            continue;
                        var outMessage = TaskExecute?.Invoke(message);
                        _channel.WriteDown(outMessage);
                    }
                },
                TaskCreationOptions.LongRunning);
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }

        public void Publish<T>(T message)
        {
        }

        public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<TOut> RequestAsync<TIn, TOut>(TIn message)
        {
            throw new NotImplementedException();
        }

        public void LoadMessageMetaData(IEnumerable<Type> types)
        {
            throw new NotImplementedException();
        }
    }
}