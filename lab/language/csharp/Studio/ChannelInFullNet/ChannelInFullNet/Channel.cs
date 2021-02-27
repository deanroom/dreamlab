using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ChannelInFullNet
{
    public class Channel
    {
        private readonly ConcurrentQueue<Action> _taskQueue;
        private readonly ConcurrentQueue<Message> _upChannel;
        private readonly ConcurrentQueue<Message> _downChannel;
        private readonly ConcurrentQueue<PubMessage> _pubChannel;

        public Channel(
            ConcurrentQueue<Message> upChannel,
            ConcurrentQueue<Message> downChannel,
            ConcurrentQueue<PubMessage> pubChannel,
            ConcurrentQueue<Action> taskQueue)
        {
            _upChannel = upChannel;
            _downChannel = downChannel;
            _pubChannel = pubChannel;
            _taskQueue = taskQueue;
        }

        public virtual InMessage ReadUp()
        {
            if (!_upChannel.TryDequeue(out var message))
                return null;
            return message as InMessage;
        }

        public virtual OutMessage ReadDown()
        {
            if (!_downChannel.TryDequeue(out var message))
                return null;
            return message as OutMessage;
        }

        public virtual void WriteUp(InMessage message)
        {
            _upChannel.Enqueue(message);
        }

        public virtual void WriteDown(OutMessage message)
        {
            _downChannel.Enqueue(message);
        }

        public virtual void WritePub(PubMessage message)
        {
            _pubChannel.Enqueue(message);
        }

        public virtual PubMessage ReadPub()
        {
            if (!_upChannel.TryDequeue(out var message))
                return null;
            return message as PubMessage;
        }

        public void Execute(Action action)
        {
            _taskQueue.Enqueue(action);
        }
    }
}