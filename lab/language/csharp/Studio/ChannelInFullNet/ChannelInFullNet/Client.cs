using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChannelInFullNet
{
    public class Client : IClient
    {
        private readonly Channel _channel;
        public event EventHandler<PubMessage> SubscribedDataReceived;

        public Client(Channel channel)
        {
            _channel = channel;

            Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        var message = _channel.ReadPub();
                        SubscribedDataReceived?.Invoke(this, message);
                    }
                },
                TaskCreationOptions.LongRunning);
        }

        public void Dispose()
        {
        }

        public void Connect(string requestAddress, string subAddress)
        {
        }

        public TOut Request<TIn, TOut>(TIn message) where TIn : InMessage where TOut : OutMessage
        {
            _channel.WriteUp(message);
            while (true)
            {
                var outMessage = _channel.ReadDown();
                if (outMessage == null)
                {
                    continue;
                }

                return outMessage as TOut;
            }
        }

        public async Task<TOut> RequestAsync<TIn, TOut>(TIn message) where TIn : InMessage where TOut : OutMessage
        {
           return  await Task.Run(()=> Request<TIn, TOut>(message)).ConfigureAwait(false);
        }

        public void LoadMessageMetaData(IEnumerable<Type> types)
        {
            throw new NotImplementedException();
        }
    }
}