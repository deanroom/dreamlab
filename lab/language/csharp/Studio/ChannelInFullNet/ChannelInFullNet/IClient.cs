using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChannelInFullNet
{
    public interface IClient:IDisposable
    {
        void Connect(string requestAddress, string subAddress);
        TOut Request<TIn, TOut>(TIn message) where TIn : InMessage where TOut : OutMessage;
        Task<TOut> RequestAsync<TIn, TOut>(TIn message) where TIn : InMessage where TOut : OutMessage;
        event EventHandler<PubMessage> SubscribedDataReceived;
        void LoadMessageMetaData(IEnumerable<Type> types);
    }

}