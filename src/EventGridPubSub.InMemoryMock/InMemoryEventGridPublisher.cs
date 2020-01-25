using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventGridPubSub.Publisher;
using EventGridPubSub.Types;

namespace EventGridPubSub.InMemoryMock
{
    public class InMemoryEventGridPublisher<T> : IEventGridPublisher<T>
        where T : PubSubMessage
    {
        private readonly List<Action<T>> _callbacks = new List<Action<T>>();

        public void Subscribe(Action<T> callback)
        {
            _callbacks.Add(callback);
        }

        public Task PublishAsync(T message)
        {
            foreach (var callback in _callbacks)
            {
                callback(message);
            }

            return Task.CompletedTask;
        }
    }
}