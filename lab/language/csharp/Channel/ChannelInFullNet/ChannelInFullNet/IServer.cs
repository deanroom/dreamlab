using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChannelInFullNet
{
    public interface IServer:IDisposable
    {
        void Connect(string requestAddress, string subAddress);
        void Execute(Action action);
        void Run();
        void RunAsync();
        void Terminate();
        event Func<InMessage, OutMessage> TaskExecute;
        void Publish<T>(T message);
        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default(CancellationToken));
        Task<TOut> RequestAsync<TIn, TOut>(TIn message);
        void LoadMessageMetaData(IEnumerable<Type> types);
    }

    
}